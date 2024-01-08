using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadScroll : MonoBehaviour
{
    // Start is called before the first frame update
    public List<MeshRenderer> MatList;
    public float Speed = 0.2f;
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();        
    }
    private void OnEnable()
    {
        //StartCoroutine(CheckView());
    }
    IEnumerator CheckView()
    {
        yield return new WaitForSeconds(0.5f);
        float km = GameManager.Instance.GetKm();
        if (km > 10000)
        {
            if (renderer.material.name != MatList[8].material.name)
                renderer.material = MatList[8].material;
        }
        else if (km > 5000)
        {
            if (renderer.material.name != MatList[9].material.name)
                renderer.material = MatList[7].material;
        }
        else if (km > 4000)
        {
            if (renderer.material.name != MatList[6].material.name)
                renderer.material = MatList[6].material;        }
        else if (km > 3000)
        {
            if (renderer.material.name != MatList[5].material.name)
                renderer.material = MatList[5].material;
        }
        else if (km > 2000)
        {
            if (renderer.material.name != MatList[4].material.name)
                renderer.material = MatList[4].material;
        }
        else if (km > 1000)
        {
            if (renderer.material.name != MatList[3].material.name)
                renderer.material = MatList[3].material;
        }
        else if (km > 500)
        {
            if (renderer.material.name != MatList[2].material.name)
                renderer.material = MatList[2].material;
        }
        else if (km > 100)
        {
            if (renderer.material.name != MatList[1].material.name)
                renderer.material = MatList[1].material;
        }
        else
        {
            if(renderer.material.name != MatList[0].material.name)
                renderer.material = MatList[0].material;
        }
        StartCoroutine(CheckView());
    }
    // Update is called once per frame
    void Update()
    {
      
        if (Speed < 0)
            Speed = 0;
        if (Speed > 4)
            Speed = 4;
        Vector2 offset = new Vector2(0, -(Time.time * Speed)/2);
        renderer.material.mainTextureOffset = offset;
        renderer.sortingLayerName = "Particle";       
    }
}
