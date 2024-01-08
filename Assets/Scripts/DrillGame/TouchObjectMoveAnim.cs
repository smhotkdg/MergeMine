using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObjectMoveAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve MoveCurve;
    public Transform _targetTrans;
    private Vector3 _target;
    private Vector3 _startPoint;
    private float _animationTimePosition;

    void Start()
    {
        
    }
    float speed = 1;
    private void OnEnable()
    {
        _target = _targetTrans.localPosition;
        _animationTimePosition = 0;
        speed = Random.Range(1f, 2.5f);
        UpdatePath();

    }

    private void UpdatePath()
    {
        _startPoint = new Vector3(0,0,0);
        //_target = Random.insideUnitSphere;
    }
    // Update is called once per frame
    public bool bStart = false;
    void Update()
    {
        if (_target != transform.localPosition)
        {
            _animationTimePosition += Time.deltaTime / speed;
            transform.localPosition = Vector3.Lerp(_startPoint, _target, MoveCurve.Evaluate(_animationTimePosition));
        }
        else
        {
            UpdatePath();
            _animationTimePosition = 0;
            this.gameObject.SetActive(false);
        }            
    }
}
