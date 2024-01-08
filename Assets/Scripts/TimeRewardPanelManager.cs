using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeRewardPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text MoneyText;
    public void SetNormal()
    {
        GameManager.Instance.totalMoney += ownMoney;
        UIManager.Instance.SetMoney();
        CollectionParticleManager.Instance.StartCoinParticle(20);
        SoundsManager.Instance.CoinsSound(10);
        UIManager.Instance.SetTimeRewardPanel(false);
    }
    public void SetAds()
    {
#if !UNITY_EDITOR
        GameManager.Instance.timerewardDoublemoney = ownMoney * 2;
        AdManager.Instance.ShowRewardedAds(1234);   
#endif

#if UNITY_EDITOR
        GameManager.Instance.timerewardDoublemoney = ownMoney * 2;
        GameManager.Instance.adsIndex = 1234;
        GameManager.Instance.AdsRewardedAdCompleted();        
#endif

    }
    public void SetAdsTutorial()
    {
        GameManager.Instance.timerewardDoublemoney = ownMoney * 2;
        GameManager.Instance.adsIndex = 1234;
        GameManager.Instance.AdsRewardedAdCompleted();
    }
    public void RewardAdsComplete()
    {
        //GameManager.Instance.AdsRewardedAdCompleted();        
    }
    double ownMoney = 0;
    public void InitData(float time)
    {
        float NowTime = time;
        if (NowTime > GameManager.Instance.Maxtime)
        {
            NowTime = GameManager.Instance.Maxtime;
        }
        ownMoney = GameManager.Instance.RewardMinerPower * NowTime;
        MoneyText.text = GameManager.Instance.ChangeFormat(ownMoney);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
