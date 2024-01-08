using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeManager : MonoBehaviour
{

    Collider2D myCollider;
    public float percent;
    void Start()
    {
        myCollider = transform.GetComponent<Collider2D>();
    }
    public float GetPercent()
    {
        return percent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
    GameObject NodeObj;
    public void deleteObject()
    {
        if(NodeObj!=null)
        {            
            NodeObj.SetActive(false);            
            //StartCoroutine(EndAnim());
        }
    }
    public void deleteObjectoops()
    {
        NodeObj.SetActive(false);
    }
  

    private void OnTriggerStay2D(Collider2D collision)
    {
        percent = BoundsContainedPercentage(myCollider.bounds, collision.bounds);
        NodeObj = collision.gameObject;
    }
    private float BoundsContainedPercentage(Bounds obj, Bounds region)
    {
        var total = 1f;
        for (var i = 0; i < 2; i++)
        {
            var dist = obj.min[i] > region.center[i] ?
                obj.max[i] - region.max[i] :
                region.min[i] - obj.min[i];

            total *= Mathf.Clamp01(1f - dist / obj.size[i]);
        }
        return total;
    }
}
