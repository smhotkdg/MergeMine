using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetRandomBox());
        StartCoroutine(GetMinerBox());
    }
    public void SetTimeRewardBox()
    {
        for(int i=0;i < 10; i++)
        {
            SetMinerBox();
        }
    }
    IEnumerator GetMinerBox()
    {
        yield return new WaitForSeconds(30f);
        if (GameManager.Instance.TutorialIndex >= 55)
        {   
            SetMinerBox();           
        }
        StartCoroutine(GetMinerBox());
    }
    IEnumerator GetRandomBox()
    {
        yield return new WaitForSeconds(5f);
        if(GameManager.Instance.TutorialIndex ==4 || GameManager.Instance.TutorialIndex == 5 || GameManager.Instance.TutorialIndex == 6)
        {
            if (bOpen == true)
            {
                SetRandomBox();
            }
        }
        if(GameManager.Instance.TutorialIndex >= 55)
        {
            int rand = Random.Range(0, 300);
            if (bOpen == true)
            {
                if (rand < 10)
                {
                    SetRandomBox();
                }                
            }        
        }      
        
        StartCoroutine(GetRandomBox());

    }
    public void SetRandomBox()
    {
        for(int i=0; i< GameManager.Instance.MinerPos.Length; i++)
        {
            if(GameManager.Instance.MinerPos[i] ==0)
            {
                GameManager.Instance.MinerPos[i] = GameManager.Instance.iBoxNumber;
                GameManager.Instance.AddBox(i);
                bOpen = false;
                return;
            }
        }
    }
    public void SetMinerBox()
    {
        for (int i = 0; i < GameManager.Instance.MinerPos.Length; i++)
        {
            if (GameManager.Instance.MinerPos[i] == 0)
            {
                GameManager.Instance.MinerPos[i] = GameManager.Instance.iBoxNumber;
                GameManager.Instance.AddMinerBox(i);
                bOpenMinerBox = false;
                return;
            }
        }
    }

    string parentName = string.Empty;
    bool bOpen = true;
    bool bOpenMinerBox = true;
    string MinerBoxparentName = string.Empty;
    public void OpenBox(string pName)
    {
        bOpen = true;
        parentName = pName;
        UIManager.Instance.SetBoxRewardPanel(true);
    }
    
    
    public void OpenMinerBox(string pName)
    {
        bOpenMinerBox = true;
        MinerBoxparentName = pName;
    }
    public void MinerBoxDestory()
    {
        if (MinerBoxparentName != string.Empty)
        {
            int outIndex;
            if (int.TryParse(MinerBoxparentName, out outIndex))
            {
                GameManager.Instance.DestroyBox(outIndex);
                //GameManager.Instance.MinerPos[outIndex - 1] = 0;
            }
        }
    }
    public void OtherBoxDestory()
    {
        if (parentName != string.Empty)
        {
            int outIndex;
            if (int.TryParse(parentName, out outIndex))
            {
                GameManager.Instance.DestroyBox(outIndex);
                //GameManager.Instance.MinerPos[outIndex - 1] = 0;
            }
        }
    }
    public void GetNormal()
    {
        SetDeleteBox(1);
    }
    void SetDeleteBox(int index)
    {
        if (parentName != string.Empty)
        {
            int outIndex;
            if (int.TryParse(parentName, out outIndex))
            {
                GameManager.Instance.AddMinerByBox(outIndex,index);
                //GameManager.Instance.MinerPos[outIndex - 1] = 0;
            }
        }
        UIManager.Instance.SetBoxRewardPanel(false);
    }
    public void GetAds()
    {
        //ads로 넘겨야함
#if !UNITY_EDITOR
        AdManager.Instance.ShowRewardedAds(5050);
#endif

#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 5050;
        GameManager.Instance.AdsRewardedAdCompleted();
#endif
    }
    public void GetAdsTutorial()
    {
        GameManager.Instance.adsIndex = 5050;
        GameManager.Instance.AdsRewardedAdCompleted();
    }
    public void RewardSucessBox()
    {
        SetDeleteBox(2);
    }
}
