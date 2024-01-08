using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinArrowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        bCheck = false;
    }

    bool bCheck = false;
    public void CheckSpin()
    {
        bCheck = true;
    }
}
