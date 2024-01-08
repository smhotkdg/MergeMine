using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSrc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TopMap;
    void Start()
    {
        StartCoroutine(testRoutine());
    }
    IEnumerator testRoutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log(TopMap.GetComponent<RectTransform>().rect);
        StartCoroutine(testRoutine());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
