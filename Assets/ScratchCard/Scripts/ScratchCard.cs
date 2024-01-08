using UnityEngine;
using UnityEngine.Rendering;

public class ScratchCard : MonoBehaviour
{
	public Camera MainCamera;
	public Transform Surface;
	public Quality RenderTextureQuality = Quality.Medium;
	public Material Eraser;
	public Material Progress;
	public Material ScratchSurface;
	
	public enum Quality
	{
		Low = 4,
		Medium = 2,
		High = 1
	}
	
	public bool IsScratching
	{
		get
		{
			foreach (var scratching in isScratching)
			{
				if (scratching)
					return true;
			}
			return false;
		}
	}
	private Mesh mesh;
	private Mesh quadMesh;
	private CommandBuffer commandBuffer;
	private RenderTargetIdentifier rti;

	private RenderTexture renderTexture;
	private Renderer scratchRenderer;
	private RectTransform rectTransform;
	private Vector2 scratchBoundsSize;
	private Vector2 imageSize;
	private Vector2[] eraseStartPositions;
	private Vector2[] eraseEndPositions;
	private Vector2 erasePosition;
	private bool isCanvasCamera;
	private bool isCanvasOverlay;
	private bool isFirstFrame = true;
	private bool[] isScratching;
	private bool[] isStartPosition;
	private int lastFrameId;

	private const string MaskTexProperty = "_MaskTex";
	private const string MainTexProperty = "_MainTex";
	private const string SourceTexProperty = "_SourceTex";
	private const int TouchMaxCount = 10;
	
	void Start()
	{
		eraseStartPositions = new Vector2[TouchMaxCount];
		eraseEndPositions = new Vector2[TouchMaxCount];
		isScratching = new bool[TouchMaxCount];
		isStartPosition = new bool[TouchMaxCount];
		for (int i = 0; i < isStartPosition.Length; i++)
		{
			isStartPosition[i] = true;
		}
		
		commandBuffer = new CommandBuffer();
		commandBuffer.name = "ScratchCard";

		quadMesh = new Mesh();
		quadMesh.vertices = new Vector3[4];
		quadMesh.uv = new[]
		{
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(1, 0),
			new Vector2(0, 0),
		};
		quadMesh.triangles = new[]
		{
			0, 1, 2,
			2, 3, 0
		};
		quadMesh.colors = new[]
		{
			Color.white,
			Color.white,
			Color.white,
			Color.white
		};
		
		GetScratchBounds();
		CreateRenderTexture();
	}

	void Update()
	{
		if (lastFrameId == Time.frameCount)
		{
			return;
		}
		
		UpdateInput();

		if (isFirstFrame)
		{
			commandBuffer.SetRenderTarget(rti);
			commandBuffer.ClearRenderTarget(false, true, Color.clear);
			Graphics.ExecuteCommandBuffer(commandBuffer);
			isFirstFrame = false;
		}

		for (int i = 0; i < isScratching.Length; i++)
		{
			var scratching = isScratching[i];
			if (scratching)
			{
				if (eraseStartPositions[i] == eraseEndPositions[i])
				{
					ScratchHole();
				}
				else
				{
					ScratchLine(i);
				}
			}
		}
		lastFrameId = Time.frameCount;
	}

	private void UpdateInput()
	{
		if (!Input.touchSupported || Input.mousePresent)
		{
			if (Input.GetMouseButtonDown(0))
			{
				isScratching[0] = false;
				isStartPosition[0] = true;
			}
			if (Input.GetMouseButtonUp(0))
			{
				isScratching[0] = false;
			}
			if (Input.GetMouseButton(0))
			{
				OnScratch(0, Input.mousePosition);
			}
		}
		if (Input.touchSupported)
		{
			foreach (Touch touch in Input.touches)
			{
				var fingerId = touch.fingerId + 1;
				if (touch.phase == TouchPhase.Began)
				{
					isScratching[fingerId] = false;
					isStartPosition[fingerId] = true;
				}

				if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					OnScratch(fingerId, touch.position);
				}

				if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					isScratching[fingerId] = false;
				}
			}
		}
	}

	private void OnScratch(int fingerId, Vector2 position)
	{
		Vector3 clickPosition;
		if (isCanvasOverlay)
		{
			clickPosition = position;
		}
		else if (isCanvasCamera)
		{
			if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, position, MainCamera, out clickPosition))
				return;
		}
		else
		{
			clickPosition = MainCamera.ScreenToWorldPoint(position);
		}
		var surfaceLocalClickPosition = Surface.InverseTransformPoint(clickPosition);
		var clickLocalPosition = new Vector3(surfaceLocalClickPosition.x * Surface.lossyScale.x, surfaceLocalClickPosition.y * Surface.lossyScale.y);
		var boundsSize = new Vector3(scratchBoundsSize.x * Surface.lossyScale.x, scratchBoundsSize.y * Surface.lossyScale.y);
		Vector2 scratchSurfaceClickLocalPosition;
		if (isCanvasCamera)
		{
			scratchSurfaceClickLocalPosition = clickLocalPosition + (Vector3)scratchBoundsSize / 2f;
		}
		else
		{
			var bottomLeftLocalPosition = Surface.InverseTransformPoint(Surface.position - boundsSize / 2f);
			scratchSurfaceClickLocalPosition = clickLocalPosition - bottomLeftLocalPosition;
		}
		var PPI = new Vector2(
			imageSize.x / scratchBoundsSize.x / Surface.lossyScale.x,
			imageSize.y / scratchBoundsSize.y / Surface.lossyScale.y
			);
		
		erasePosition = new Vector2(
			scratchSurfaceClickLocalPosition.x * Surface.lossyScale.x * PPI.x,
			scratchSurfaceClickLocalPosition.y * Surface.lossyScale.y * PPI.y
			);

		if (isStartPosition[fingerId])
		{
			eraseEndPositions[fingerId] = eraseStartPositions[fingerId];
			eraseStartPositions[fingerId] = erasePosition;
		}
		else
		{
			eraseEndPositions[fingerId] = erasePosition;
		}
		isStartPosition[fingerId] = !isStartPosition[fingerId];
		
		if (!isScratching[fingerId])
		{
			eraseEndPositions[fingerId] = eraseStartPositions[fingerId];
			isScratching[fingerId] = true;
		}
	}

	private void CreateRenderTexture()
	{
		var renderTextureSize = new Vector2(imageSize.x / (float) RenderTextureQuality, imageSize.y / (float) RenderTextureQuality);
		renderTexture = new RenderTexture((int) renderTextureSize.x, (int) renderTextureSize.y, 0, RenderTextureFormat.ARGB32);
		renderTexture.Create();
		ScratchSurface.SetTexture(MaskTexProperty, renderTexture);
		Progress.SetTexture(MainTexProperty, renderTexture);
		if (Progress.HasProperty(SourceTexProperty))
		{
			Progress.SetTexture(SourceTexProperty, ScratchSurface.mainTexture);
		}
		rti = new RenderTargetIdentifier(renderTexture);
	}

	private void GetScratchBounds()
	{
		scratchRenderer = Surface.GetComponent<Renderer>();
		rectTransform = Surface.GetComponent<RectTransform>();
		if (scratchRenderer != null)
		{
			imageSize = new Vector2(scratchRenderer.sharedMaterial.mainTexture.width, scratchRenderer.sharedMaterial.mainTexture.height);
			scratchBoundsSize = scratchRenderer.bounds.size;
		}
		else if (rectTransform != null)
		{
			imageSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
			scratchBoundsSize = new Vector2(rectTransform.rect.size.x * rectTransform.lossyScale.x, 
			                                rectTransform.rect.size.y * rectTransform.lossyScale.y);

			var canvas = Surface.transform.GetComponentInParent<Canvas>();
			if (canvas != null)
			{
				isCanvasOverlay = canvas.renderMode == RenderMode.ScreenSpaceOverlay;
				isCanvasCamera = !isCanvasOverlay;
			}
		}
		else
		{
			Debug.LogError("Can't find Renderer or RectTransform Component!");
		}
	}

	private void ScratchHole()
	{
		var positionRect = new Rect(
			(erasePosition.x - 0.5f * Eraser.mainTexture.width) / imageSize.x,
			(erasePosition.y - 0.5f * Eraser.mainTexture.height) / imageSize.y,
			Eraser.mainTexture.width / imageSize.x,
			Eraser.mainTexture.height / imageSize.y
			);
		
		quadMesh.vertices = new[]
		{
			new Vector3(positionRect.xMin, positionRect.yMax, 0),
			new Vector3(positionRect.xMax, positionRect.yMax, 0),
			new Vector3(positionRect.xMax, positionRect.yMin, 0),
			new Vector3(positionRect.xMin, positionRect.yMin, 0)
		};
		
		GL.LoadOrtho();
		commandBuffer.Clear();
		commandBuffer.SetRenderTarget(rti);
		commandBuffer.DrawMesh(quadMesh, Matrix4x4.identity, Eraser);
		Graphics.ExecuteCommandBuffer(commandBuffer);
	}
	
	private void ScratchLine(int fingerId)
	{
		var holesCount = (int)Vector2.Distance(eraseStartPositions[fingerId], eraseEndPositions[fingerId]) / (int)RenderTextureQuality;
		var positions = new Vector3[holesCount * 4];
		var colors = new Color[holesCount * 4];
		var indices = new int[holesCount * 6];
		var uv = new Vector2[holesCount * 4];

		for (int i = 0; i < holesCount; i++)
		{
			var holePosition = eraseStartPositions[fingerId] + (eraseEndPositions[fingerId] - eraseStartPositions[fingerId]) / holesCount * i;
			var positionRect = new Rect(
				(holePosition.x - 0.5f * Eraser.mainTexture.width) / imageSize.x,
				(holePosition.y - 0.5f * Eraser.mainTexture.height) / imageSize.y,
				Eraser.mainTexture.width / imageSize.x,
				Eraser.mainTexture.height / imageSize.y
				);
			
			positions[i * 4 + 0] = new Vector3(positionRect.xMin, positionRect.yMax, 0);
			positions[i * 4 + 1] = new Vector3(positionRect.xMax, positionRect.yMax, 0);
			positions[i * 4 + 2] = new Vector3(positionRect.xMax, positionRect.yMin, 0);
			positions[i * 4 + 3] = new Vector3(positionRect.xMin, positionRect.yMin, 0);

			colors[i * 4 + 0] = Color.white;
			colors[i * 4 + 1] = Color.white;
			colors[i * 4 + 2] = Color.white;
			colors[i * 4 + 3] = Color.white;

			uv[i * 4 + 0] = Vector2.up;
			uv[i * 4 + 1] = Vector2.one;
			uv[i * 4 + 2] = Vector2.right;
			uv[i * 4 + 3] = Vector2.zero;

			indices[i * 6 + 0] = 0 + i * 4;
			indices[i * 6 + 1] = 1 + i * 4;
			indices[i * 6 + 2] = 2 + i * 4;
			indices[i * 6 + 3] = 2 + i * 4;
			indices[i * 6 + 4] = 3 + i * 4;
			indices[i * 6 + 5] = 0 + i * 4;
		}

		if (positions.Length > 0)
		{
			if (mesh != null)
			{
				mesh.Clear(false);
			}
			else
			{
				mesh = new Mesh();
			}

			mesh.vertices = positions;
			mesh.uv = uv;
			mesh.triangles = indices;
			mesh.colors = colors;
			GL.LoadOrtho();
			commandBuffer.Clear();
			commandBuffer.SetRenderTarget(rti);
			commandBuffer.DrawMesh(mesh, Matrix4x4.identity, Eraser);
			Graphics.ExecuteCommandBuffer(commandBuffer);
		}
	}
	
    public void Reset()
    {
        CreateRenderTexture();
        isFirstFrame = true;
    }
}