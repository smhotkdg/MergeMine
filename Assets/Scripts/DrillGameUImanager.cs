using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrillGameUImanager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ChangeViewStart;
    public GameObject ChangeViewEnd;
    public Text ScoreText;
    public Text TopSocreText;
    public Text GetGoldText;
    public Text GetGoldthreeTimeText;


    public Text TimeText;
    public GameObject EndDrillkingPanel;
    public QuadScroll myquadScroll;
    public List<GameObject> DrillKIngDrillList;
    public GameObject rythmObj;
    public Text KmText;


    public GameObject DrilKIngObj;
    public GameObject DrillKIngUIobj;
    public GameObject DrillImage;
    public List<GameObject> newDrillList;
    public List<GameObject> DrillObjList;
    public Text DrillLvText;
    public Button UpgradeButton;
    public Text PercentText;
    public Text CountText;
    public Image FillImage;
    public List<GameObject> MinerList;
    public Text MinerLvText;

    public GameObject NewDrillGetPanel;
    Color32 EnableColor = new Color32(255, 255, 255, 255);
    Color32 DisableColor = new Color32(255, 255, 255, 100);
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ChangeSpeed(float power)
    {
        myquadScroll.Speed += ((GameManager.Instance.DrillLv * power) /2);             
        //StartCoroutine(SpeedUProutine(((GameManager.Instance.DrillLv * power) / 2)));
        DrillKIngDrillList[GameManager.Instance.DrillLv - 1].GetComponent<Animator>().speed += (GameManager.Instance.DrillLv * power) / 2;        
      
        Drillpower = GameManager.Instance.DrillLv * power;
        if (Drillpower < 0)
            Drillpower = 0;
        //KmText.text = " km";
    }
    IEnumerator SpeedUProutine(float speed)
    {
        float indexer = speed / 100;
        for (int i=0;i<100; i++)
        {
            myquadScroll.Speed += indexer;
            yield return new WaitForSeconds(0.01f);
        }
    }
    bool bStartGame = false;
    float km = 0;
    float Drillpower;
    public void EndGame()
    {        
        Drillpower = 0;
        myquadScroll.Speed = 0;
        DrillKIngDrillList[GameManager.Instance.DrillLv - 1].GetComponent<Animator>().speed =0;
        
        bStartGame = false;
        SoundsManager.Instance.SetDrillKingSound(false);
    }
    public void StartGame()
    {
        SoundsManager.Instance.SetDrillKingSound(true);
        time = 30;
        bStartGame = true;
        StartCoroutine(StartGameRoutine());
    }
    float time = 30;
    IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(30);        
        SetEndDrillKing(true);
        bStartGame = false;
    }
    
    void Update()
    {
        if(bStartGame ==true)
        {
            time = time - Time.deltaTime;
            TimeText.text = time.ToString("N0");
            km += Drillpower*Time.deltaTime *100;            
            KmText.text = km.ToString("N1") +" km";
        }
        else
        {
            TimeText.text = "";
        }
    }
    public float GetKm()
    {
        return km;
    }
    public void InitData()
    {
        for(int i=0; i< MinerList.Count; i++)
        {
            if(MinerList[i].activeSelf == true)
            {
                MinerList[i].SetActive(false);
            }
        }
        for (int i = 0; i < DrillObjList.Count; i++)
        {
            if (DrillObjList[i].activeSelf == true)
            {
                DrillObjList[i].SetActive(false);
            }
        }
        
        DrillObjList[GameManager.Instance.DrillLv - 1].SetActive(true);
        DrillLvText.text = "Lv. " + (GameManager.Instance.DrillLv).ToString();
        int MaxMiner = GameManager.Instance.MaxMergetNumber;
        int drillindex = (GameManager.Instance.DrillLv+1) * 5;
        if (drillindex < 10)
            drillindex = 10;
        if (MinerList.Count > drillindex)
        {
            MinerList[drillindex - 1].SetActive(true);
        }   
        MinerLvText.text = (drillindex).ToString();     

        CountText.text = GameManager.Instance.TotalNutCount + " / " + GameManager.Instance.DrillUpgradeNutCount[GameManager.Instance.DrillLv - 1];
        double percent = (GameManager.Instance.TotalNutCount / GameManager.Instance.DrillUpgradeNutCount[GameManager.Instance.DrillLv - 1]);
        PercentText.text = (percent * 100).ToString("N0") + " %";
        FillImage.fillAmount = (float)percent;

        if(MaxMiner >= drillindex && GameManager.Instance.TotalNutCount >= GameManager.Instance.DrillUpgradeNutCount[GameManager.Instance.DrillLv - 1])
        {
            UpgradeButton.interactable = true;
            UpgradeButton.transform.Find("DrillKingText").GetComponent<Text>().color = EnableColor;
        }
        else
        {
            UpgradeButton.interactable = false;
            UpgradeButton.transform.Find("DrillKingText").GetComponent<Text>().color = DisableColor;
        }
    }

    public void UpgradeDrill()
    {
        GameManager.Instance.TotalNutCount -= GameManager.Instance.DrillUpgradeNutCount[GameManager.Instance.DrillLv - 1];
        if (GameManager.Instance.TotalNutCount < 0)
            GameManager.Instance.TotalNutCount = 0;
        GameManager.Instance.DrillLv++;        
        if (GameManager.Instance.totalDrillCount >=GameManager.Instance.DrillLv)
        {
            for(int i =0; i< newDrillList.Count;i++)
            {
                if(newDrillList[i].activeSelf ==true)
                {
                    newDrillList[i].SetActive(false);
                }
            }
            newDrillList[GameManager.Instance.DrillLv - 1].SetActive(true);
            NewDrillGetPanel.SetActive(true);
            InitData();
        }
    }   
    public void ExitNewDrillPanel()
    {
        NewDrillGetPanel.SetActive(false);

    }
    public void StartDrillKing()
    {
        //여기서 오픈 이펙트
        ChangeViewStart.SetActive(true);
        DrilKIngObj.SetActive(true);
        DrillKIngUIobj.SetActive(false);
        rythmObj.SetActive(false);
        myquadScroll.Speed = 0;
        for(int i=0; i< DrillKIngDrillList.Count; i++)
        {
            if (DrillKIngDrillList[i].activeSelf == true)
                DrillKIngDrillList[i].SetActive(false);
        }
        DrillKIngDrillList[GameManager.Instance.DrillLv - 1].SetActive(true);
        DrillKIngDrillList[GameManager.Instance.DrillLv - 1].GetComponent<Animator>().speed = 0;
        KmText.text = "0 km";
        
    }
    public void StartAnimEnd()
    {
        rythmObj.SetActive(true);
        StartGame();
    }
    public void EndAnimEnd()
    {
        if(bNormal == true)
        {
            UIManager.Instance.SetMoney();
            CollectionParticleManager.Instance.StartCoinParticle(20);
            SoundsManager.Instance.CoinsSound(10);
        }
        bNormal = false;
    }
    public void ExitDrillking()
    {
        ChangeViewEnd.SetActive(true);
        DrilKIngObj.SetActive(false);
        DrillKIngUIobj.SetActive(true);
        EndGame();
    }
    public void SetEndDrillKing(bool flag)
    {

        EndDrillkingPanel.SetActive(flag);
        rythmObj.SetActive(!flag);
        if(flag == true)
        {
            GetGoldText.text = GameManager.Instance.ChangeFormat(GameManager.Instance.GetNowGoldPerSec() * (km / 2));
            GetGoldthreeTimeText.text = GameManager.Instance.ChangeFormat(GameManager.Instance.GetNowGoldPerSec() * (km / 2) *3);
            GameManager.Instance.DrillGold = GameManager.Instance.GetNowGoldPerSec() * (km / 2) * 3;
            if (GameManager.Instance.TopDigScore <km)
            {
                GameManager.Instance.TopDigScore = km;
            }
            TopSocreText.text = "Top socre : " + "<color=red>" +(GameManager.Instance.TopDigScore).ToString("N1")+"</color>" +" Km";
            ScoreText.text = km.ToString("N1") + " km";
        }
        if(flag == false)
        {
            ExitDrillking();
        }
    }
    bool bNormal = false;
    public void GetNormal()
    {
        bNormal = true;        
        GameManager.Instance.DrillGold = 0;
        GameManager.Instance.totalMoney += GameManager.Instance.GetNowGoldPerSec() * (km /2);
        SetEndDrillKing(false);
        km = 0;
    }
    public void GetAds()
    {
#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 99999;
        GameManager.Instance.AdsRewardedAdCompleted();
#endif
#if !UNITY_EDITOR
    AdManager.Instance.ShowRewardedAds(99999);
#endif  
        SetEndDrillKing(false);
        km = 0;
    }
    public void RewardAds()
    {

    }
}
