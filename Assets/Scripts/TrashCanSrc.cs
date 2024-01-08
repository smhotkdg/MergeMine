using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanSrc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TrashCan1;
    public GameObject TrashCan2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Miner")
        {
            TrashCan1.SetActive(false);
            TrashCan2.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Miner")
        {
            TrashCan1.SetActive(true);
            TrashCan2.SetActive(false);
        }       
    }
}
