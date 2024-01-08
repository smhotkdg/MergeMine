using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

  

    public void Click()
    {
        if (this.transform.childCount > 1)
        {
            if(this.transform.GetChild(0).GetComponent<DragDrop>() == true)
                this.transform.GetChild(0).GetComponent<DragDrop>().OnResetMiner();            
        }
    }
    public void SetMinerNumber()
    {
        this.transform.Find("TextMinerNumber").gameObject.SetActive(true);
        if(this.transform.childCount > 1)
        {
            int outIndex;
            if(int.TryParse(this.transform.GetChild(0).name,out outIndex))
            {
                this.transform.Find("TextMinerNumber").gameObject.GetComponent<Text>().text = this.transform.GetChild(0).name;
            }            
        }        
    }
    public void DisableNumber()
    {
        this.transform.Find("TextMinerNumber").gameObject.SetActive(false);
    }
}
