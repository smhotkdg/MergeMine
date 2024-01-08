using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTextGoldAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.SetParent(parent.transform);
    }
}
