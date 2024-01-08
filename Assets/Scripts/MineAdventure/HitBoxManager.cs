using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Collider[] colls;
    private void OnTriggerStay2D(Collider2D collision)
    {
        colls = Physics.OverlapSphere(transform.position, 100.0f);
    }
}
