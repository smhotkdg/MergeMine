using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;
public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
   
    private static AdManager _instance = null;
    AudienceNetworkClientImpl fbadClient;
    AdMobClientImpl admobClient;
    public static AdManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton AdManager == null");
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }
    void Start()
    {
        // AdMob client.
        admobClient = Advertising.AdMobClient;
        // Facebook Audience Network client.
        fbadClient = Advertising.AudienceNetworkClient;


        fbadClient.RewardedAdCompleted += FbanClient_RewardedAdCompleted;
        admobClient.RewardedAdCompleted += AdmobClient_RewardedAdCompleted;
    }

    private void AdmobClient_RewardedAdCompleted(IAdClient arg1, AdPlacement arg2)
    {
        //admob rewardcomplete
        GameManager.Instance.AdsRewardedAdCompleted();
    }

    private void FbanClient_RewardedAdCompleted(IAdClient arg1, AdPlacement arg2)
    {
        //FireBaseRewardComplete
        GameManager.Instance.AdsRewardedAdCompleted();
    }
 

    // Update is called once per frame
    void Update()
    {        
        if (fbadClient.IsRewardedAdReady())
        {            
        }    
        if (admobClient.IsRewardedAdReady())
        {            
        }
      
    }
    public bool CheckReady()
    {
        if (fbadClient.IsRewardedAdReady() == true)
        {
            return true;
        }        
        else if (admobClient.IsRewardedAdReady() == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ShowRewardedAds(int index)
    {
        if(GameManager.Instance.isNoads ==false)
        {
            GameManager.Instance.adsIndex = index;
            if (fbadClient.IsRewardedAdReady())
            {
                fbadClient.ShowRewardedAd();
            }
            else
            {
                if (admobClient.IsRewardedAdReady())
                {
                    admobClient.ShowRewardedAd();
                }
            }
        }
        else
        {
            GameManager.Instance.adsIndex = index;
            GameManager.Instance.AdsRewardedAdCompleted();
        }
    }
     
}
