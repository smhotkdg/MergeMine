using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBoxSrc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ClickMinerBox()
    {
        GameManager.Instance.myRandomBox.OpenMinerBox(this.transform.parent.name);
        SoundsManager.Instance.BoxSound();
        GameManager.Instance.DestroyMinerBox(this.transform.parent.name);

        if (GameManager.Instance.MaxMergetNumber - 9 > 0)
        {
            GameManager.Instance.AddMiner(GameManager.Instance.MaxMergetNumber - 8, int.Parse(this.transform.parent.name)-1,true);
        }
        else
        {
            GameManager.Instance.AddMiner(1, int.Parse(this.transform.parent.name) - 1,true);
        }
    }
}
