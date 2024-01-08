using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldrushManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject GoldImage;
    public Text GoldRushCoinText;
    public GameObject TimeText;
    public GameObject GoldRushButtonObj;
    public GameObject CloseButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void SetGoldRush(bool flag)
    {
        GoldImage.SetActive(flag);
        GoldRushCoinText.text = "";
        GoldRushButtonObj.SetActive(flag);
        TimeText.SetActive(!flag);
    }
    void Update()
    {
        if(bStartrush == false)
        {
            if (GameManager.Instance.GoldRushTime <= 0)
            {
                SetGoldRush(true);
            }
            else
            {
                TimeText.GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.GoldRushTime, "#00:00");
                SetGoldRush(false);
            }
        }       
    }
    bool bStartrush = false;
    bool bParticle = true;
    double GetMoney = 0;
    IEnumerator StartGoldRush()
    {
        CloseButton.SetActive(false);
        SoundsManager.Instance.TrainStartSound();
        TimeText.SetActive(false);
        GoldImage.SetActive(true);
        GoldRushCoinText.text = "";
        //SetGoldRush();
        yield return new WaitForSeconds(1f);        
        SoundsManager.Instance.TrainSound();        
        this.GetComponent<Animator>().SetBool("isGoldrush", true);        
        CollectionParticleManager.Instance.StartCoinParticle(20);
        SoundsManager.Instance.CoinsSound(5);
        StartCoroutine(ParticleShow());
        GetMoney = GameManager.Instance.GetNowGoldPerSec() * 1800;
        tempmoney = GetMoney / 64f;
    }
    public void GoGoldRrush()
    {     
#if !UNITY_EDITOR
        AdManager.Instance.ShowRewardedAds(12989);
#endif

#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 12989;
        GameManager.Instance.AdsRewardedAdCompleted();
#endif
    }
    public void CompleteAds()
    {
        bStartrush = true;
        GoldRushButtonObj.SetActive(false);
        GameManager.Instance.GoldRushTime = GameManager.Instance.DefaultGoldRushTime;
        StartCoroutine(StartGoldRush());
    }
    private void OnDisable()
    {
        SoundsManager.Instance.StopTrain();
        bStartrush = false;
        tempmoney = 0;
        bParticle = true;
        TotalGetMoney = 0;
        GoldRushCoinText.text = "";
        this.GetComponent<Animator>().SetBool("isGoldrush", false);
       
    }
    public void EndTrain()
    {
        bParticle = false;
        GoldRushCoinText.text = GameManager.Instance.ChangeFormat(GetMoney);        
        CloseButton.SetActive(true);
    }    
    double tempmoney=0;
    double TotalGetMoney;
    public void SetTutorial()
    {        
        TutorialManager.Instance.EndTrainTutorial();
        bStartrush = true;
        GoldRushButtonObj.SetActive(false);
        GameManager.Instance.GoldRushTime = GameManager.Instance.DefaultGoldRushTime;
        StartCoroutine(StartGoldRush());
    }
    IEnumerator ParticleShow()
    {      
        yield return new WaitForSeconds(0.1f);
        if(bParticle ==true)
        {
            CollectionParticleManager.Instance.StartCoinParticle(5);
            SoundsManager.Instance.CoinsSound(2);            
            StartCoroutine(ParticleShow());
        }
        TotalGetMoney += tempmoney;
        GoldRushCoinText.text =GameManager.Instance.ChangeFormat(TotalGetMoney);

        GameManager.Instance.totalMoney += tempmoney;
        UIManager.Instance.SetMoney();        
    }
}
