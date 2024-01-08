using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoxRewardUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MinerReward;
    public GameObject SpeedReward;
    public GameObject GoldReward;

    //public Text NormalGoldText;
    public Text AdsGoldText;

    public Text TextNormalMinerNumber;
    public Text TextAdsRewardMinerNumber;
    public List<GameObject> NormalMinerList;
    public List<GameObject> AdsMinerList;
    void Start()
    {
        setWeightNoraml();
    }
    public List<int> MineralWeightNormal = new List<int>();
    public Dictionary<int, int> weightsNormal = new Dictionary<int, int>();
    public void setWeightNoraml()
    {
        if (weightsNormal.Count == 3)
            return;
        for (int i = 0; i < 3; i++)
        {
            int index = i + 1;
            MineralWeightNormal.Add(index);
        }
        weightsNormal.Add(0, 20);
        weightsNormal.Add(1, 50);
        weightsNormal.Add(2, 30);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.AdsGoldTime <= 0)
        {
            GoldReward.transform.Find("BG/TimeReward").gameObject.GetComponent<Button>().interactable = true;
            GoldReward.transform.Find("BG/TimeReward/GetitText").gameObject.GetComponent<Text>().text = "GET IT !!";
        }
        else
        {            
            GoldReward.transform.Find("BG/TimeReward").gameObject.GetComponent<Button>().interactable = false;
            GoldReward.transform.Find("BG/TimeReward/GetitText").gameObject.GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.AdsGoldTime, "#00:00");            
        }


        if (GameManager.Instance.AdsSpeedUpTime <= 0)
        {
            SpeedReward.transform.Find("TimeReward").gameObject.GetComponent<Button>().interactable = true;
            SpeedReward.transform.Find("SecText").gameObject.GetComponent<Text>().text = "GET IT !!";
        }
        else
        {
            SpeedReward.transform.Find("TimeReward").gameObject.GetComponent<Button>().interactable = false;
            SpeedReward.transform.Find("SecText").gameObject.GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.AdsSpeedUpTime, "#00:00");
        }

    }
    void SetMinerBox()
    {
        MinerReward.SetActive(true);
        for (int i = 0; i < NormalMinerList.Count; i++)
        {
            NormalMinerList[i].SetActive(false);
        }
        for (int i = 0; i < AdsMinerList.Count; i++)
        {
            AdsMinerList[i].SetActive(false);
        }
        if (GameManager.Instance.MaxMergetNumber - 5 > 0)
        {
            TextNormalMinerNumber.text = (GameManager.Instance.MaxMergetNumber - 5).ToString();
            NormalMinerList[GameManager.Instance.MaxMergetNumber - 5 - 1].SetActive(true);
        }
        else
        {
            TextNormalMinerNumber.text = 1.ToString();
            NormalMinerList[1 - 1].SetActive(true);
        }

        if (GameManager.Instance.MaxMergetNumber - 3 > 0)
        {
            TextAdsRewardMinerNumber.text = (GameManager.Instance.MaxMergetNumber - 3).ToString();
            AdsMinerList[GameManager.Instance.MaxMergetNumber - 3 - 1].SetActive(true);
        }
        else
        {
            TextAdsRewardMinerNumber.text = GameManager.Instance.MaxMergetNumber.ToString();
            AdsMinerList[GameManager.Instance.MaxMergetNumber - 1].SetActive(true);
        }
    }
    public void SetSpeedUP()
    {
#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 115588;
        GameManager.Instance.AdsRewardedAdCompleted();
        
#endif
#if !UNITY_EDITOR
    AdManager.Instance.ShowRewardedAds(115588);
    
#endif
    }
    public void SetSpeedUPTuroial()
    {
        GameManager.Instance.adsIndex = 115588;
        GameManager.Instance.AdsRewardedAdCompleted();
    }
    void SetSpeedBox()
    {
        SpeedReward.SetActive(true);
    }
    double RewardGold;
    public void GetNormal()
    {
        UIManager.Instance.SetBoxRewardPanel(false);
        CollectionParticleManager.Instance.StartCoinParticle(7);
        SoundsManager.Instance.CoinsSound(5);
        GameManager.Instance.totalMoney += GameManager.Instance.BoxRewardMoney;
        GameManager.Instance.BoxRewardMoney = 0;
        UIManager.Instance.SetMoney();
        RewardGold = 0;
    }
    public void GetAds()
    {
#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 5588;
        GameManager.Instance.AdsRewardedAdCompleted();
        
#endif
#if !UNITY_EDITOR
    AdManager.Instance.ShowRewardedAds(5588);
    
#endif
    }
    public void GetAdsTutorial()
    {
        GameManager.Instance.adsIndex = 5588;
        GameManager.Instance.AdsRewardedAdCompleted();
    }
    void SetGoldBox()
    {
        GoldReward.SetActive(true);
        RewardGold = GameManager.Instance.GetNowGoldPerSec() * 300;
        GameManager.Instance.BoxRewardMoney = RewardGold;
        //NormalGoldText.text = GameManager.Instance.ChangeFormat(RewardGold);
        AdsGoldText.text = GameManager.Instance.ChangeFormat(RewardGold *3);
    }
    private void Awake()
    {
        if (weightsNormal.Count == 0)
            setWeightNoraml();
    }
    private void OnEnable()
    {
        if (weightsNormal.Count == 0)
            setWeightNoraml();
    }
    public void SetBoxReward(int index)
    {
        MinerReward.SetActive(false);
        SpeedReward.SetActive(false);
        GoldReward.SetActive(false);
        switch (index)
        {
            case 1:
                SetSpeedBox();
                break;
            case 2:
                SetGoldBox();
                break;
        }

    }
    public void SetBoxReward()
    {
        MinerReward.SetActive(false);
        SpeedReward.SetActive(false);
        GoldReward.SetActive(false);
        if (GameManager.Instance.TutorialIndex >= 55)
        {
            //if (weightsNormal.Count == 0)
            //    setWeightNoraml();
            //int normalMinerIndex = WeightedRandomizer.From(weightsNormal).TakeOne();

            //switch (normalMinerIndex)
            //{
            //    case 0:
            //        SetMinerBox();
            //        break;
            //    case 1:
            //        if (GameManager.Instance.bStartAdsSpeedUp == true)
            //        {
            //            RewardGold = GameManager.Instance.GetNowGoldPerSec() * 300;
            //            GameManager.Instance.BoxRewardMoney = RewardGold;
            //            SetGoldBox();
            //        }
            //        else
            //        {
            //            SetSpeedBox();
            //        }

            //        break;
            //    case 2:
            //        RewardGold = GameManager.Instance.GetNowGoldPerSec() * 300;
            //        GameManager.Instance.BoxRewardMoney = RewardGold;
            //        SetGoldBox();
            //        break;
            //}
            SetMinerBox();
        }
        else if (GameManager.Instance.TutorialIndex == 4 || GameManager.Instance.TutorialIndex == 5 || GameManager.Instance.TutorialIndex == 6)
        {
            switch (GameManager.Instance.TutorialIndex)
            {
                case 4:
                    SetMinerBox();
                    TutorialManager.Instance.SetAds1Tutorial();
                    break;
                case 5:
                    SetSpeedBox();
                    TutorialManager.Instance.SetAds2Tutorial();
                    break;
                case 6:
                    RewardGold = GameManager.Instance.GetNowGoldPerSec() * 300;
                    GameManager.Instance.BoxRewardMoney = RewardGold;
                    SetGoldBox();
                    TutorialManager.Instance.SetAds3Tutorial();
                    break;
            }
        }
    }
}
