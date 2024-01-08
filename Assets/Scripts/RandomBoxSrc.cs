using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxSrc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClickRandomBox()
    {
        GameManager.Instance.myRandomBox.OpenBox(this.transform.parent.name);
        SoundsManager.Instance.BoxSound();
    }

}
