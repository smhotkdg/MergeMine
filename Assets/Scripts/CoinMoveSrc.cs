using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoveSrc : MonoBehaviour
{
    
    public AnimationCurve MoveCurve;
    public Transform _targetTrans;
    private Vector3 _target;
    private Vector3 _startPoint;
    private float _animationTimePosition;    

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        
        bEndClick = false;
        _target = _targetTrans.localPosition;
        UpdatePath();
  
    }

    bool bEndClick = false;
    private int myIndex = -1;
    public void SetIndex(int index)
    {
        myIndex = index;
    }


    private void Update()
    {
        if (_target != transform.localPosition )        {
           
            _animationTimePosition += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(_startPoint, _target, MoveCurve.Evaluate(_animationTimePosition));
        
        }
        else
        {
            if(bEndClick == false)
            {
                UpdatePath();
                _animationTimePosition = 0;
                bEndClick = true;
                this.gameObject.SetActive(false);
            }
            
        }
    }

    private void UpdatePath()
    {
        _startPoint = transform.localPosition;
        //_target = Random.insideUnitSphere;
    }
}
