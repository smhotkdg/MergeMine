using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using System.IO;
#endif
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update   
    public bool isNoads;
    public int DigPower;
    public List<int> DrillPartList;
    public int DrillAdventureTotalDigCount;
    public int DrillAdventureTotalDigRangeCount;
    public int DrillAdventureTotalDigCount_Now;
    public int DrillAdventureCount;
    public List<GameObject> GemList;
    public List<GameObject> StarCoinList;
    public int totalGem;
    public int totalStarCoin;
    public double TopDigScore;
    public DrillGameUImanager myDrillUIManager;
    public double TotalNutCount;
    public int DrillLv;
    public List<double> DrillUpgradeNutCount;
    public List<double> DrillPower;
    public int totalDrillCount;
    public List<GameObject> NutList;
    public GameObject GoldParent;
    public Material DefaultMat;
    public Material EffectMat;    
    public List<GameObject>MineList;
    public GameObject TopDragPos;
    public GetMinerManager MyGetMinerManager;
    private static GameManager _instance = null;
    public List<GameObject> GetMinerParticlePool;
    public List<GameObject> TextEffectPool;
    public int TextEffectCount;
    public GameObject ParticleParent;
    public int TotalMergecount = 12;
    public int MaxMiner = 60;
    public int[] MinerPos ;
    public bool[] MinerPosMap;
    public Vector3 [] MinerMapVector;
    public double totalMoney;
    public int MinerGetParticlePoolIndex = 0;
    public List<double> TotalGetMinerCount;
    public List<double> GainPerSecMiner;
    public List<double> DefaultMinerCost;
    public RandomBoxManager myRandomBox;
    public int iBoxNumber = 54875;    
    public bool BGM;
    public bool Fx;
    public int Language_Type;
    public double timerewardDoublemoney = 0;
    public bool Istutorial;
    int MineCount = 0;
    public bool isSpin;
    public float SpinTime;
    public float GoldRushTime;
    public float FreeGoldBarTime;
    public float DefaultFreeGoldBarTime;
    public float MaxSpinTime;
    public float DefaultGoldRushTime;

    public float DefaultAdsGoldTime;
    public float AdsGoldTime;

    public float DefaultAdsSpeedUpTime;
    public float AdsSpeedUpTime;

    public int [] posLock;
    public bool bEndSpinTutorial;
    public bool bEndTrainTutorial;

    public bool bEndShopTutorial;
    public bool bEndDrillAdventureTutorial;


    public bool bEndAdsGold;
    public bool BEndAdsSpeed;

    public List<double> MinerBuffPower;
    int NutCount = 0;
    public GameObject GetNut(GameObject myObj)
    {
        if (NutList.Count - 1 <= NutCount)
            NutCount = 0;
        NutCount++;
        GameObject _nut = NutList[NutCount];
        Image MyImage = _nut.GetComponent<Image>();
        MyImage.color = new Color32(255, 255, 255, 255);
        if (MyImage == null)
            return null;
        _nut.GetComponent<Image>().color = MyImage.color;
        //Vector3 spawnPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //spawnPostion.z = 0;        
        _nut.transform.SetParent(myObj.transform);
        _nut.transform.localPosition = new Vector3(0, 0, 0);
        _nut.transform.SetParent(GoldParent.transform);
        return _nut;
    }
    public int GemCount;
    public GameObject GetGem(GameObject myObj)
    {
        if (GemList.Count - 1 <= GemCount)
            GemCount = 0;
        GemCount++;
        GameObject _Gem = GemList[GemCount];
        Image MyImage = _Gem.GetComponent<Image>();
        MyImage.color = new Color32(255, 255, 255, 255);
        _Gem.GetComponent<Image>().color = MyImage.color;
        if (MyImage == null)
            return null;
        //Vector3 spawnPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //spawnPostion.z = 0;        
        _Gem.transform.SetParent(myObj.transform);
        _Gem.transform.localPosition = new Vector3(0, 0, 0);
        _Gem.transform.SetParent(GoldParent.transform);
        return _Gem;
    }
    int StarCoinCount;
    public GameObject GetStarCoin(GameObject myObj)
    {
        if (StarCoinList.Count - 1 <= StarCoinCount)
            StarCoinCount = 0;
        StarCoinCount++;
        GameObject _starCoin = StarCoinList[StarCoinCount];
        Image MyImage = _starCoin.GetComponent<Image>();
        if (MyImage == null)
            return null;
        MyImage.color = new Color32(255, 255, 255, 255);
        _starCoin.GetComponent<Image>().color = MyImage.color;
        //Vector3 spawnPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //spawnPostion.z = 0;        
        _starCoin.transform.SetParent(myObj.transform);
        _starCoin.transform.localPosition = new Vector3(0, 0, 0);
        _starCoin.transform.SetParent(GoldParent.transform);
        return _starCoin;
    }

    public void EndDrillGame()
    {
        myDrillUIManager.SetEndDrillKing(true);
    }
    public void SetDrillSpeed(float speed)
    {
        myDrillUIManager.ChangeSpeed(speed);
    }
    public float GetKm()
    {
       return myDrillUIManager.GetKm();
    }
    public GameObject SetGold(GameObject myObj)
    {
        if (MineList.Count-1  <= MineCount)
            MineCount = 0;
        MineCount++;        
        GameObject _gold = MineList[MineCount];
        Image MyImage = _gold.GetComponent<Image>();
        MyImage.color = new Color32(255, 255, 255, 255);
        _gold.GetComponent<Image>().color = MyImage.color;
        //Vector3 spawnPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //spawnPostion.z = 0;        
        _gold.transform.SetParent(myObj.transform);
        _gold.transform.localPosition = new Vector3(0, 0, 0);
        _gold.transform.SetParent(GoldParent.transform);
        return _gold;
    }
    public double BoxRewardMoney;
    public bool bStartAdsSpeedUp = false;
    public int AdsGoldPower = 1;
    public void DrillUIInit()
    {
        myDrillUIManager.InitData();
    }
    public void StartDrillGame()
    {
        myDrillUIManager.StartAnimEnd();
    }
    public void EndDrillGame_Anim()
    {
        myDrillUIManager.EndAnimEnd();
    }
    public void AdsRewardedAdCompleted()
    {
        switch (adsIndex)
        {
            case 5050:
                UIManager.Instance.SetAdsRewardPanel(true);
                UIManager.Instance.AdsRewardPanel.GetComponent<AdsRewardPanelManager>().SetInitData("Miner");
                myRandomBox.RewardSucessBox();
                break;
            case 1234:
                UIManager.Instance.SetTimeRewardPanel(false);
                UIManager.Instance.SetAdsRewardPanel(true);
                UIManager.Instance.AdsRewardPanel.GetComponent<AdsRewardPanelManager>().SetInitData("Time");
                totalMoney += timerewardDoublemoney;
                UIManager.Instance.SetMoney();
                CollectionParticleManager.Instance.StartCoinParticle(30);
                SoundsManager.Instance.CoinsSound(10);
                break;
            case 5588:
                UIManager.Instance.SetBoxRewardPanel(false);
                UIManager.Instance.SetAdsRewardPanel(true);
                UIManager.Instance.AdsRewardPanel.GetComponent<AdsRewardPanelManager>().SetInitData("AdsGold");
                totalMoney += BoxRewardMoney*3;
                UIManager.Instance.SetMoney();
                CollectionParticleManager.Instance.StartCoinParticle(20);
                SoundsManager.Instance.CoinsSound(10);
                GameManager.Instance.AdsGoldTime = GameManager.Instance.DefaultAdsGoldTime;
                break;
            case 115588:
                UIManager.Instance.SetBoxRewardPanel(false);
                UIManager.Instance.SetAdsRewardPanel(true);
                UIManager.Instance.AdsRewardPanel.GetComponent<AdsRewardPanelManager>().SetInitData("SpeedUp");
                GameManager.Instance.AdsSpeedUpTime = GameManager.Instance.DefaultAdsSpeedUpTime;
                bStartAdsSpeedUp = true;
                break;
            case 66182:
                UIManager.Instance.SetSpin();
                break;
            case 12989:
                UIManager.Instance.GoldRushStart();
                break;
            case 99999:
                UIManager.Instance.SetAdsRewardPanel(true);
                UIManager.Instance.AdsRewardPanel.GetComponent<AdsRewardPanelManager>().SetInitData("AdsGold_Drill");
                totalMoney += DrillGold;
                UIManager.Instance.SetMoney();
                CollectionParticleManager.Instance.StartCoinParticle(20);
                SoundsManager.Instance.CoinsSound(10);                
                break;
        }
        DrillGold = 0;
        BoxRewardMoney = 0;
        timerewardDoublemoney = 0;
        adsIndex = 0;
    }
    public double DrillGold;
    public float GetTopDragHeight()
    {
        return TopDragPos.GetComponent<RectTransform>().rect.height;
    }
    public double GetTotalMoney()
    {
        return totalMoney;
    }
    public void AddTotalMoney(double money)
    {
        totalMoney += money;
    }
    public void SubtractTotalMoney(double money)
    {
        totalMoney -= money;
        if(totalMoney < 0)
        {
            totalMoney = 0;
        }
    }
    public List<Vector3> minerPosVector;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton GameManager == null");
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
            DontDestroyOnLoad(gameObject);
        }        
    }

    public GameObject Position;
    private void OnApplicationQuit()
    {
        SaveData();

    }
    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
            SaveData();
    }
    void SaveData()
    {
        ES3.Save<GameManager>("GameManager", _instance);       
    }
    bool LoadData()
    {
        if (ES3.KeyExists("GameManager") == true)
        {            
            _instance = ES3.Load<GameManager>("GameManager");            
            return true;
        }            
        else
        {
            totalMoney = 2000;
            isOriMember = true;
            return false;
        }
            
    }
    public double RewardMinerPower;
    public float Maxtime = 7200;
    public double GetNowGoldPerSec()
    {
        double dNowGoldPerSec = 0;
        for (int i = 0; i < MinerPos.Length; i++)
        {            
            if (MinerPos[i] > 0 && iBoxNumber!=MinerPos[i])
            {                
                dNowGoldPerSec += GainPerSecMiner[MinerPos[i] - 1];             
            }
        }
        return dNowGoldPerSec;
    }
    GameObject TutorialObj;
    public bool isOriMember = false;
    void Start()
    {
        Istutorial = false;
        Language_Type = -1;
        Initdata();
        if(LoadData())
        {
            RewardMinerPower = 0;
            for (int i=0; i< MinerPos.Length; i++)
            {
                if(MinerPos[i] == iBoxNumber)
                {
                    MinerPos[i] = 0;
                    MinerPosMap[i] = false;
                }
                if (MinerPos[i] > 0 )
                {
                    GameObject Miner = AddMinerInit(i, MinerPos[i]);
                    RewardMinerPower += GainPerSecMiner[MinerPos[i] -1];
                    if (MinerPosMap[i]== true)
                    {
                        Miner.GetComponent<DragDrop>().SetInitMap();
                    }                    
                }
            }
        }
        if (DefaultGoldRushTime != 900)
        {
            DefaultGoldRushTime = 900;
            if (GoldRushTime > DefaultGoldRushTime)
                GoldRushTime = DefaultGoldRushTime;
        }
        UpdateData();
        Maxtime = 7200;
        //On Tutorial
        //Istutorial = true;        
       
        if (isOriMember == false)
        {
            if (MinerPos[6] != 0 || MinerPos[7] != 0 || MinerPos[8] != 0 || MinerPos[9] != 0 || MinerPos[10] != 0 || MinerPos[11] != 0)
            {
                for (int i = 0; i < posLock.Length; i++)
                {
                    posLock[i] = 0;
                }
            }            
        }      
        for(int i=0; i< posLock.Length;i++)
        {
            int index = i + 6;            
            if(posLock[i] ==iBoxNumber)
            {
                MinerPos[index] = posLock[i];
                Position.transform.GetChild(index).GetComponent<BoxCollider2D>().enabled = false;
            }                            
        }
       
        UIManager.Instance.SetPosLock();
        UIManager.Instance.SetMoney();
        UIManager.Instance.SetTotalGemText();
        UIManager.Instance.SetTotalStarCoinText();
        UIManager.Instance.SetMinerView();
        UIManager.Instance.SetIcon();
        MyGetMinerManager.InitData();
        
        SoundsManager.Instance.MuteBGM();
        TimerManager.Instance.CheckRewarad();
        TimerManager.Instance.LoadTime();
        bSpeedUp = false;
        bStartAdsSpeedUp = false;

        for(int i=0; i< DrillAdventureCount;i++)
        {
            for(int k = 0; k< GainPerSecMiner.Count; k++)
            {
                GainPerSecMiner[k] = GainPerSecMiner[k] * MinerBuffPower[i];
            }
        }

        UIManager.Instance.SetBuffPower();

        if (Language_Type == -1)
        {
            string code = I2.Loc.LocalizationManager.GetLanguageCode(I2.Loc.LocalizationManager.GetCurrentDeviceLanguage());
            I2.Loc.SetLanguage setLanguage = new I2.Loc.SetLanguage();
            switch (code)
            {
                case "ko":
                    Language_Type = 1;
                    setLanguage._Language = "korean";
                    setLanguage.ApplyLanguage();
                    break;
                case "en":
                    Language_Type = 2;
                    setLanguage._Language = "English";
                    setLanguage.ApplyLanguage();
                    break;
                case "ja":
                    Language_Type = 3;
                    setLanguage._Language = "Japanese";
                    setLanguage.ApplyLanguage();
                    break;
                case "zh-TW":
                    Language_Type = 4;
                    setLanguage._Language = "Chinese";
                    setLanguage.ApplyLanguage();
                    break;
                case "zh-CN":
                    Language_Type = 5;
                    setLanguage._Language = "Chinese (Taiwan)";
                    setLanguage.ApplyLanguage();
                    break;
                case "vi":
                    Language_Type = 6;
                    setLanguage._Language = "Indonesian";
                    setLanguage.ApplyLanguage();
                    break;     
                default:
                    Language_Type = 2;
                    setLanguage._Language = "English";
                    setLanguage.ApplyLanguage();
                    break;
            }

        }
        //On Tutorial

        if (Istutorial == false)
        {
            TutorialManager.Instance.StartTutorial();
        }
    }
    public void SetTimeRewardBox()
    {
        myRandomBox.SetTimeRewardBox();
    }
    public int TutorialIndex = 0;
    void debuff()
    {
        GainPerSecMiner.Clear();
        for (int i = 0; i < MaxMiner; i++)
        {         
            double GetMoney = 100;
            for (int k = 0; k < i; k++)
            {
                GetMoney = GetMoney * 1.45f;     
            }
            GainPerSecMiner.Add((GetMoney));
            TotalGetMinerCount.Add(0);
        }
    }
    void LoadInit()
    {

    }
    public void DrillAdventureGameOver()
    {
        UIManager.Instance.SetGameOverDrillAdventure();
    }
    public List<float> Mineincrement = new List<float>();
    public void UpdateData()
    {
        totalDrillCount = 20;
        MaxMiner = 60;
        DrillAdventureTotalDigCount_Now = 0;
        MinerBuffPower.Clear();
        
        DrillUpgradeNutCount.Clear();
        DrillPower.Clear();
        Mineincrement.Clear();
        double defaultBuff = 10;
        for (int i = 0; i < 12; i++)
        {
            defaultBuff = 10;        
            MinerBuffPower.Add(defaultBuff);
        }
     
        for(int i =0; i< totalDrillCount; i++)
        {
            DrillUpgradeNutCount.Add(10);
            DrillPower.Add(55*(i+1));
        }
        for (int i = 0; i < MaxMiner+1; i++)
        {
            Mineincrement.Add(1.2f);
        }
        {
            //{
            //    Mineincrement[0] = 1.01f;
            //    Mineincrement[1] = 1.01f;
            //    Mineincrement[2] = 1.01f;
            //    Mineincrement[3] = 1.01f;
            //    Mineincrement[4] = 1.01f;
            //    Mineincrement[5] = 1.7f;
            //    Mineincrement[6] = 2.5f;
            //    Mineincrement[7] = 1.7f;
            //    Mineincrement[8] = 1.7f;
            //    Mineincrement[9] = 1.7f;
            //    Mineincrement[10] = 1.7f;
            //    Mineincrement[11] = 1.7f;
            //    Mineincrement[12] = 1.7f;
            //    Mineincrement[13] = 1.7f;
            //    Mineincrement[14] = 1.7f;
            //    Mineincrement[15] = 1.7f;
            //    Mineincrement[16] = 10f;
            //    Mineincrement[17] = 1.7f;
            //    Mineincrement[18] = 1.7f;
            //    Mineincrement[19] = 10f;
            //    Mineincrement[20] = 1.7f;
            //    Mineincrement[21] = 1.7f;
            //    Mineincrement[22] = 1.7f;
            //    Mineincrement[23] = 10f;
            //    Mineincrement[24] = 1.7f;
            //    Mineincrement[25] = 1.7f;
            //    Mineincrement[26] = 10f;
            //    Mineincrement[27] = 1.7f;
            //    Mineincrement[28] = 1.7f;
            //    Mineincrement[29] = 10f;
            //    Mineincrement[30] = 1.7f;
            //    Mineincrement[31] = 1.7f;
            //    Mineincrement[32] = 1.7f;
            //    Mineincrement[33] = 10f;
            //    Mineincrement[34] = 1.7f;
            //    Mineincrement[35] = 1.7f;
            //    Mineincrement[36] = 10f;
            //    Mineincrement[37] = 1.7f;
            //    Mineincrement[38] = 1.7f;
            //    Mineincrement[39] = 1.5f;
            //    Mineincrement[40] = 1.7f;
            //    Mineincrement[41] = 10f;
            //    Mineincrement[42] = 1.7f;
            //    Mineincrement[43] = 1.7f;
            //    Mineincrement[44] = 5f;
            //    Mineincrement[45] = 1.7f;
            //    Mineincrement[46] = 1.5f;
            //    Mineincrement[47] = 1.7f;
            //    Mineincrement[48] = 5f;
            //    Mineincrement[49] = 1.7f;
            //    Mineincrement[50] = 5f;
            //    Mineincrement[51] = 1.7f;
            //    Mineincrement[52] = 5f;
            //    Mineincrement[53] = 1.7f;
            //    Mineincrement[54] = 1.7f;
            //    Mineincrement[55] = 1.7f;
            //    Mineincrement[56] = 1.7f;
            //    Mineincrement[57] = 1.7f;
            //    Mineincrement[58] = 1.7f;
            //    Mineincrement[59] = 1.7f;
            //    Mineincrement[60] = 1.7f;
            //}
        }
        minerPosVector = new List<Vector3>();
        minerPosVector.Clear();
        minerPosVector.Clear();
        DefaultMinerCost.Clear();
        GainPerSecMiner.Clear();
        //TotalGetMinerCount.Clear();
        double defaultMinerCost = 200;
        double GetMoney = 100;
        for (int i = 0; i < MaxMiner; i++)
        {
            Vector3 myVec = new Vector3();
            minerPosVector.Add(myVec);        
            //for (int k = 0; k < i; k++)
            //{
            DefaultMinerCost.Add(defaultMinerCost);
            GainPerSecMiner.Add((GetMoney));

            GetMoney = GainPerSecMiner[i] * Mineincrement[i+1];
            defaultMinerCost = (DefaultMinerCost[i] * 3f);
   
        }        
        {
            minerPosVector[0] = new Vector3(-14, 20.6f, 0);
            minerPosVector[1] = new Vector3(-15, 16, 0);
            minerPosVector[2] = new Vector3(-15, 16, 0);
            minerPosVector[3] = new Vector3(-12, 16, 0);
            minerPosVector[4] = new Vector3(-17, 7, 0);
            minerPosVector[5] = new Vector3(-15, 5, 0);
            minerPosVector[6] = new Vector3(-15, 5, 0);
            minerPosVector[7] = new Vector3(-12, 7, 0);
            minerPosVector[8] = new Vector3(-16, 7, 0);
            minerPosVector[9] = new Vector3(-16, 7, 0);
            minerPosVector[10] = new Vector3(-10, 17, 0);
            minerPosVector[11] = new Vector3(-10, 17, 0);
            minerPosVector[12] = new Vector3(-12, 15, 0);
            minerPosVector[13] = new Vector3(-12, 15, 0);
            minerPosVector[14] = new Vector3(-25, 15, 0);
            minerPosVector[15] = new Vector3(-25, 15, 0);
            minerPosVector[16] = new Vector3(-20, 13, 0);
            minerPosVector[17] = new Vector3(-20, 13, 0);
            minerPosVector[18] = new Vector3(-10, 17, 0);
            minerPosVector[19] = new Vector3(-10, 17, 0);
            minerPosVector[20] = new Vector3(-24, 12, 0);
            minerPosVector[21] = new Vector3(-17, 11, 0);
            minerPosVector[22] = new Vector3(-30, 10, 0);
            minerPosVector[23] = new Vector3(-30, 10, 0);
            minerPosVector[24] = new Vector3(-38, 20, 0);
            minerPosVector[25] = new Vector3(-23, 13, 0);
            minerPosVector[26] = new Vector3(-9, 19, 0);
            minerPosVector[27] = new Vector3(-20, 19, 0);
            minerPosVector[28] = new Vector3(-27, -18, 0);
            minerPosVector[29] = new Vector3(-23, -9.5f, 0);
            minerPosVector[30] = new Vector3(-22, 18, 0);
            minerPosVector[31] = new Vector3(-18, 16, 0);
            minerPosVector[32] = new Vector3(-10, 14, 0);
            minerPosVector[33] = new Vector3(-18, 10, 0);
            minerPosVector[34] = new Vector3(-14, 11, 0);
            minerPosVector[35] = new Vector3(-17, 10, 0);
            minerPosVector[36] = new Vector3(-14, 15, 0);
            minerPosVector[37] = new Vector3(-19, 5, 0);
            minerPosVector[38] = new Vector3(-24, -2, 0);
            minerPosVector[39] = new Vector3(-23, -2, 0);
            minerPosVector[40] = new Vector3(-28, 15, 0);
            minerPosVector[41] = new Vector3(-22, 15, 0);
            minerPosVector[42] = new Vector3(-13, 5, 0);
            minerPosVector[43] = new Vector3(-37, 24, 0);
            minerPosVector[44] = new Vector3(-46, 17, 0);
            minerPosVector[45] = new Vector3(-9, 10, 0);
            minerPosVector[46] = new Vector3(-43, 11, 0);
            minerPosVector[47] = new Vector3(-16, 13, 0);
            minerPosVector[48] = new Vector3(-18, 15, 0);
            minerPosVector[49] = new Vector3(-14, 12, 0);
            minerPosVector[50] = new Vector3(-18, 11, 0);
            minerPosVector[51] = new Vector3(-25, 9, 0);
            minerPosVector[52] = new Vector3(-18, 13, 0);
            minerPosVector[53] = new Vector3(-20, 14, 0);
            minerPosVector[54] = new Vector3(-18, 13, 0);
            minerPosVector[55] = new Vector3(-19, 15, 0);
            minerPosVector[56] = new Vector3(-15, 11, 0);
            minerPosVector[57] = new Vector3(-14, 12, 0);
            minerPosVector[58] = new Vector3(-18, 17, 0);
            minerPosVector[59] = new Vector3(-21, 10, 0);
        }
        for (int i = 0; i < minerPosVector.Count; i++)
        {
            Vector3 tempVec = minerPosVector[i];
            tempVec.y += 25;
            minerPosVector[i] = tempVec;
        }

#if UNITY_EDITOR

        for (int i = 0; i < DefaultMinerCost.Count; i++)
        {
            //Debug.Log("defalut miner Cost  " + ChangeFormat(DefaultMinerCost[i]));
            WriteStringDefalutCost(DefaultMinerCost[i].ToString("N3"));

        }
        for (int i = 0; i < GainPerSecMiner.Count; i++)
        {
            //Debug.Log("GainPerSecMiner  " + ChangeFormat(GainPerSecMiner[i]));
            WriteString(GainPerSecMiner[i].ToString("N3"));
        }
#endif
    }
    void Initdata()
    {
        isNoads = false;
        bEndTrainTutorial = false;
        bEndSpinTutorial = false;
        bEndShopTutorial = false;
        bEndDrillAdventureTutorial =false;
        bEndAdsGold = false;
        BEndAdsSpeed = false;
        DrillAdventureTotalDigCount = 7;
        DrillAdventureTotalDigRangeCount = 1;
        totalGem = 0;
        totalStarCoin = 0;
        DigPower = 1;
        DrillAdventureCount = 0;
        isSpin = true;
        BGM = true;
        Fx = true;
        TotalNutCount = 0;
        MaxSpinTime = 300;
        DefaultGoldRushTime = 7200;
        DefaultFreeGoldBarTime = 3600;
        DrillLv = 1;
        GoldRushTime = DefaultGoldRushTime;

        DefaultAdsGoldTime = 300;
        DefaultAdsSpeedUpTime = 300;


        FreeGoldBarTime = 0;
        SpinTime = MaxSpinTime;
        Language_Type = 1;
        posLock = new int[6];
        for(int i=0; i< posLock.Length;i++)
        {
            posLock[i] = iBoxNumber;
        }
        MinerPosMap = new bool[TotalMergecount];
        MinerPos = new int[TotalMergecount];
        MinerMapVector = new Vector3[TotalMergecount];
        for (int i = 0; i < MinerPosMap.Length; i++)
        {
            MinerPosMap[i] = false;
        }
        MaxMergetNumber = 1;
        for(int i =0; i< MaxMiner; i++)
        {
            TotalGetMinerCount.Add(0);
        }
        for(int i=0; i< 20;i++)
        {
            DrillPartList.Add(0);
        }
    }
#if UNITY_EDITOR
    void WriteString(string text)
    {
        string path = "Assets/Resources/GainPerSecMiner.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

    }
    void WriteStringDefalutCost(string text)
    {
        string path = "Assets/Resources/DefaultCost.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

    }
#endif

    public int minerNumber = 1;
    public int GetminerLevel()
    {
        return minerNumber;
    }

    public void ColliderChange(bool flag,int index)
    {
        if(flag ==false)
        {
            for (int i = 0; i < Position.transform.childCount; i++)
            {
                if (MinerPos[i] != index)
                {
                    if(Position.transform.GetChild(i).childCount >0)
                    {
                        int resultint = 0;
                        if (int.TryParse(Position.transform.GetChild(i).GetChild(0).name, out resultint))
                        {
                            Position.transform.GetChild(i).GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Position.transform.childCount; i++)
            {
                if (Position.transform.GetChild(i).childCount > 0)
                {
                    int resultint = 0;
                    if(int.TryParse(Position.transform.GetChild(i).GetChild(0).name,out resultint))
                    {
                        Position.transform.GetChild(i).GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                    }                    
                }              
            }
        }
    
    }
    public void UpgradeMinerGetLevel()
    {
        if (minerNumber > MaxMiner)
        {
            minerNumber = MaxMiner;
            return;
        }
            
        minerNumber++;
        UIManager.Instance.SetMinerView();
        MyGetMinerManager.UpdateCost();
        MyGetMinerManager.SetNextButton();
        
    }
    public void MinerLevevlDown()
    {
        minerNumber--;
        if (minerNumber <= 0)
        {
            minerNumber = 1;
            return;
        }
        UIManager.Instance.SetMinerView();
        MyGetMinerManager.UpdateCost();
        MyGetMinerManager.SetNextButton();
        
    }
    public void SetMInerCollider(bool flag)
    {
        for(int i=0; i< TotalMergecount; i++)
        {
            int MinerCount = i + 1;
            string strPos = MinerCount.ToString();
            GameObject Pos = Position.transform.Find(strPos).gameObject;
            GameObject ch = Pos.transform.GetChild(0).gameObject;
            int outindex;
            if(int.TryParse(ch.name,out outindex))
            {
                if(ch.GetComponent<BoxCollider2D>() !=null)
                    ch.GetComponent<BoxCollider2D>().enabled = flag;
            }
            
        }
    }
    public bool bSpeedUp = false;
    public void SetSpeedUP()
    {
        if(bSpeedUp ==false)
        {
            bSpeedUp = true;
            for (int i = 0; i < MinerPos.Length; i++)
            {

                if (MinerPos[i] > 0 && iBoxNumber != MinerPos[i])
                {                    
                    string strPos = (i+1).ToString();
                    GameObject Pos = Position.transform.Find(strPos).gameObject;
                    GameObject Miner = Pos.transform.GetChild(0).gameObject;
                    //if (MinerPosMap[i] == true)
                    {
                        Miner.GetComponent<MinerGoldSrc>().SetSpeedUP();                        
                    }
                    if(MinerPosMap[i] == true)
                    {
                        Miner.GetComponent<MinerGoldSrc>().SetEffect();
                    }
                }
            }
            StartCoroutine(SpeedUpRoutine());
            UIManager.Instance.SetSpeedUpText(true);
            SpeedTime = 120;
        }    
    }
    public float SpeedTime = 0;
    IEnumerator SpeedUpRoutine()
    {
        yield return new WaitForSeconds(120f);
        UIManager.Instance.SetSpeedUpText(false);
        bSpeedUp = false;
        bStartAdsSpeedUp = false;
        for (int i = 0; i < MinerPos.Length; i++)
        {
            if (MinerPos[i] > 0 && iBoxNumber != MinerPos[i])
            {
                string strPos = (i + 1).ToString();
                GameObject Pos = Position.transform.Find(strPos).gameObject;
                GameObject Miner = Pos.transform.GetChild(0).gameObject;
                //if (MinerPosMap[i] == true)
                {
                    Miner.GetComponent<MinerGoldSrc>().EndSpeedUP();
                    Miner.GetComponent<MinerGoldSrc>().SetEffect();
                }
            }
        }        
    }
    public int adsIndex = 0;
    public void GetMIner()
    {
        if (minerNumber > MaxMiner)
            return;
        if(CheckFullMiner())
        {
            if (totalMoney >= MyGetMinerManager.GetCost())
            {
                AddMiner(minerNumber);
                SubtractTotalMoney(MyGetMinerManager.GetCost());
                UIManager.Instance.SetMoney();
                TotalGetMinerCount[minerNumber - 1]++;
            }
        }     
    }
    public void AddBox(int minerPos)
    {
        int MinerCount = minerPos;     
        
        MinerCount = MinerCount + 1;
        string strPos = MinerCount.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        string MinerName = "Box/RandomBox";
        GameObject MinerObjPrefab = Resources.Load(MinerName) as GameObject;
        GameObject BoxObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = false;        
        BoxObj.transform.SetParent(Pos.transform);
        BoxObj.transform.SetAsFirstSibling();

        //MinerObj.transform.localPosition = new Vector2(0, 0);
        BoxObj.transform.name = "Box";
        BoxObj.transform.localPosition = new Vector3(0, 10, 0);
        BoxObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public void AddMinerBox(int minerPos)
    {
        int MinerCount = minerPos;

        MinerCount = MinerCount + 1;
        string strPos = MinerCount.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        string MinerName = "Box/MinerBox";
        GameObject MinerObjPrefab = Resources.Load(MinerName) as GameObject;
        GameObject BoxObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = false;
        BoxObj.transform.SetParent(Pos.transform);
        BoxObj.transform.SetAsFirstSibling();

        //MinerObj.transform.localPosition = new Vector2(0, 0);
        BoxObj.transform.name = "MinerBox";
        BoxObj.transform.localPosition = new Vector3(0, 10, 0);
        BoxObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }
    GameObject AddMinerInit(int pPos,int MinerIndex)
    {
        GameObject Miner = AddMinerObj(MinerIndex, pPos);
        return Miner;
    }
    public void DestroyBox(int boxindex)
    {
        GameObject Pos = Position.transform.Find(boxindex.ToString()).gameObject;
        GameObject box = Pos.transform.Find("Box").gameObject;
        if (box == null)
            return;
        Pos.GetComponent<BoxCollider2D>().enabled = true;
        MinerPos[boxindex - 1] = 0;
        Destroy(box);
    }

    public void DestroyMinerBox(string boxindex)
    {
        GameObject Pos = Position.transform.Find(boxindex).gameObject;
        GameObject box = Pos.transform.Find("MinerBox").gameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = true;
        MinerPos[int.Parse(boxindex) - 1] = 0;
        Destroy(box);
    }

    public void AddMinerByBox(int pPos,int boxtype)
    {
        GameObject Pos = Position.transform.Find(pPos.ToString()).gameObject;
        GameObject box = Pos.transform.Find("Box").gameObject;
        switch (boxtype)
        {
            //normalbox
            case 1:
                if(MaxMergetNumber - 5 >0)
                {
                    AddMiner(MaxMergetNumber - 5, pPos-1);
                }
                else
                {
                    AddMiner(1, pPos - 1);
                }                
                break;
            //ads box;
            case 2:
                if (MaxMergetNumber - 3 > 0)
                {
                    AddMiner(MaxMergetNumber - 3, pPos - 1);
                }
                else
                {
                    AddMiner(MaxMergetNumber, pPos - 1);
                }
                break;
        }

        Destroy(box);
    }
    GameObject AddMinerObj(int minerNumber, int pPos)
    {
        MinerPos[pPos] = minerNumber;
        pPos = pPos + 1;
        string strPos = pPos.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        string MinerName = "Prefabs/Miner" + minerNumber.ToString();
        GameObject MinerObjPrefab = Resources.Load(MinerName) as GameObject;
        GameObject MinerObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = false;
        MinerObj.name = minerNumber.ToString();
        MinerObj.transform.SetParent(Pos.transform);
        MinerObj.transform.SetAsFirstSibling();

        MinerObj.transform.parent.GetComponent<DropParent>().SetMinerNumber();
        //MinerObj.transform.localPosition = new Vector2(0, 0);
        MinerObj.transform.localPosition = minerPosVector[minerNumber - 1];
        return MinerObj;
    }
    public void AddMiner(int minerNumber,int pPos,bool isParticle = false)
    {     
        MinerPos[pPos] = minerNumber;
        pPos = pPos + 1;
        string strPos = pPos.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        string MinerName = "Prefabs/Miner" + minerNumber.ToString();
        GameObject MinerObjPrefab = Resources.Load(MinerName) as GameObject;
        GameObject MinerObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = false;
        MinerObj.name = minerNumber.ToString();
        MinerObj.transform.SetParent(Pos.transform);
        MinerObj.transform.SetAsFirstSibling();

        MinerObj.transform.parent.GetComponent<DropParent>().SetMinerNumber();
        //MinerObj.transform.localPosition = new Vector2(0, 0);
        MinerObj.transform.localPosition = minerPosVector[minerNumber - 1];

        Vector2 ParticleVec = new Vector2(0, 0);
        if (isParticle == true)
        {
            if (GetMinerParticlePool.Count - 1 < MinerGetParticlePoolIndex)
            {
                MinerGetParticlePoolIndex = 0;
            }

            GetMinerParticlePool[MinerGetParticlePoolIndex].transform.SetParent(MinerObj.transform.parent);
            GetMinerParticlePool[MinerGetParticlePoolIndex].transform.localPosition = ParticleVec;
            GetMinerParticlePool[MinerGetParticlePoolIndex].GetComponent<FlatFX>().AddEffect(GetMinerParticlePool[MinerGetParticlePoolIndex].transform.position, 2);
            MinerGetParticlePoolIndex++;
        }
    }
    public bool CheckFullMiner()
    {
        int MinerCount = -1;

        for (int i = TotalMergecount - 1; i >= 0; i--)
        {
            if (MinerPos[i] == 0)
            {
                MinerCount = i;
            }
        }
        if (MinerCount == -1)
            return false;
        return true;
    }
    void AddMiner(int minerNumber)
    {
        int MinerCount = -1;
        
        for(int i= TotalMergecount-1; i>= 0; i--)
        {
            if (MinerPos[i] == 0)
            {
                MinerCount = i;
            }
        }
        if (MinerCount == -1)
            return;        
        MinerPos[MinerCount] = minerNumber;
        MinerCount = MinerCount + 1;
        string strPos = MinerCount.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        string MinerName = "Prefabs/Miner" + minerNumber.ToString();
        GameObject MinerObjPrefab = Resources.Load(MinerName) as GameObject;
        GameObject MinerObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = false;
        MinerObj.name = minerNumber.ToString();
        MinerObj.transform.SetParent(Pos.transform);
        MinerObj.transform.SetAsFirstSibling();

        MinerObj.transform.parent.GetComponent<DropParent>().SetMinerNumber();
        //MinerObj.transform.localPosition = new Vector2(0, 0);
        MinerObj.transform.localPosition = minerPosVector[minerNumber-1];
        Vector2 ParticleVec = new Vector2(0, 0);
        //Vector2 ParticleVec = MinerObj.transform.parent.GetComponent<RectTransform>().anchoredPosition;
  
    }
    public int MaxMergetNumber;
    public bool SetMerge(string name)
    {
        GameObject Pos = Position.transform.Find(name).gameObject;
        if (Pos.transform.childCount <= 0)
            return false;
        GameObject Child = Pos.transform.GetChild(0).gameObject;
        int index = int.Parse(Child.name);
        index++;
        string prepabId = "Prefabs/Miner" + index;
        if(index >MaxMergetNumber)
        {           
            MaxMergetNumber = index;
            UIManager.Instance.SetNewMinerPanel(true);
            UIManager.Instance.SetMinerView();            
        }
        MyGetMinerManager.SetNextButton();
        GameObject MinerObjPrefab = Resources.Load(prepabId) as GameObject;
        if (MinerObjPrefab == null)
        {
            Debug.Log("Final Object");
            return false;
        }
        else
        {            
            if(TutorialIndex ==1)
            {
                TutorialManager.Instance.SetMergeMInerTutorial();
            }
            GameObject MinerObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
            if (MinerObj == null)
                return false;
            MinerObj.name = index.ToString();
            MinerObj.transform.SetParent(Pos.transform);
            MinerObj.transform.SetAsFirstSibling();
            MinerPos[int.Parse(name) - 1] = index;            
            //MinerObj.transform.localPosition = new Vector2(0, 0);
            MinerObj.transform.localPosition =minerPosVector[index-1];
            MinerObj.SetActive(false);
            
            MergeAnim(index - 1, Pos.transform,MinerObj);            
            Destroy(Child);
            return true;
        }

    }
  
    void MergeAnim(int MergeIndex,Transform parent,GameObject MinerObj)
    {
        //x +-0.4
        string prepabId = "Prefabs/Miner" + MergeIndex;
        GameObject MinerObjPrefab = Resources.Load(prepabId) as GameObject;
        if (MinerObjPrefab == null)
            return;
        GameObject MinerObj_temp1 = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        GameObject MinerObj_temp2 = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        
        MinerObj_temp1.transform.SetParent(parent);
        MinerObj_temp2.transform.SetParent(parent);
        MinerObj_temp1.transform.localScale = MinerObj.transform.localScale;
        MinerObj_temp2.transform.localScale = MinerObj.transform.localScale;
        Vector3 LeftPos = minerPosVector[MergeIndex - 1];
        LeftPos.x = LeftPos.x - 25;
        MinerObj_temp1.transform.localPosition = LeftPos;

        Vector3 RightPos = minerPosVector[MergeIndex - 1];
        RightPos.x = RightPos.x + 25;
        MinerObj_temp2.transform.localPosition = RightPos;

        StartCoroutine(MoveAnim(MinerObj_temp1,MinerObj_temp2, MinerObj));
    }
    IEnumerator MoveAnim(GameObject M1, GameObject M2,GameObject MinerObj)
    {
        
        for(int i=0; i< 3; i++)
        {
            Vector3 MoveVec = M1.transform.localPosition;
            MoveVec.x -= 3/ 25;
            M1.transform.localPosition = MoveVec;

            MoveVec = M2.transform.localPosition;
            MoveVec.x += 3 / 25;
            M2.transform.localPosition = MoveVec;
            yield return new WaitForSeconds(0.005f);
        }
        float speed = 5f;
        for (int i = 0; i < 5; i++)
        {
            Vector3 MoveVec = M1.transform.localPosition;
            MoveVec.x += speed;
            M1.transform.localPosition = MoveVec;

            MoveVec = M2.transform.localPosition;
            MoveVec.x -= speed;
            M2.transform.localPosition = MoveVec;
            yield return new WaitForSeconds(0.005f);
            speed += 1f;
            if(i ==5)
            {
                Vector2 ParticleVec = new Vector2(0,0);
                //Vector2 ParticleVec = MinerObj.transform.parent.GetComponent<RectTransform>().anchoredPosition;
                if (GetMinerParticlePool.Count-1 < MinerGetParticlePoolIndex)
                {
                    MinerGetParticlePoolIndex = 0;
                }
                
                GetMinerParticlePool[MinerGetParticlePoolIndex].transform.SetParent(MinerObj.transform.parent);
                GetMinerParticlePool[MinerGetParticlePoolIndex].transform.localPosition = ParticleVec;
                GetMinerParticlePool[MinerGetParticlePoolIndex].GetComponent<FlatFX>().AddEffect(GetMinerParticlePool[MinerGetParticlePoolIndex].transform.position, 2);
            }
        }
        Destroy(M1);
        Destroy(M2);
        MinerObj.SetActive(true);
        SoundsManager.Instance.MergeSound();
        MinerObj.transform.parent.GetComponent<DropParent>().SetMinerNumber();

        for (int i=0; i< 5; i++)
        {
            if(MinerObj !=null)
            {
                Vector3 ScaleVec = MinerObj.transform.localScale;
                ScaleVec.x += 10f;
                ScaleVec.y += 10f;
                MinerObj.transform.localScale = ScaleVec;
                yield return new WaitForSeconds(0.005f);
            }
            else
            {
                Debug.Log("Coroutine MinerObj Null");
            }
        }        
        for (int i = 0; i < 5; i++)
        {
            if(MinerObj !=null)
            {
                Vector3 ScaleVec = MinerObj.transform.localScale;
                ScaleVec.x -= 10f;
                ScaleVec.y -= 10f;
                MinerObj.transform.localScale = ScaleVec;
                yield return new WaitForSeconds(0.005f);
            }
            else
            {
                Debug.Log("Coroutine MinerObj Null");
            }
                 
        }
        if (GetMinerParticlePool.Count - 1 < MinerGetParticlePoolIndex)
        {
            MinerGetParticlePoolIndex = 0;
        }
        GetMinerParticlePool[MinerGetParticlePoolIndex].transform.SetParent(ParticleParent.transform);
        MinerGetParticlePoolIndex++;
    }
    public void SetInitMiner(string strName)
    {
        GameObject Pos = Position.transform.Find(strName).gameObject;
        Pos.transform.GetChild(0).gameObject.GetComponent<DragDrop>().OnResetMiner();
    }
    public void SetCollider(int index)
    {
        string strPos = index.ToString();
        GameObject Pos = Position.transform.Find(strPos).gameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void TrashObj(string NowPos, string MinerCount)
    {
        MinerPos[int.Parse(NowPos) - 1] = 0;
        MinerPosMap[int.Parse(NowPos) - 1] = false;
    }
    public void ChangePos(string NowPos,string NextPos, string MinerCount)
    {
        MinerPos[int.Parse(NowPos) - 1] = 0;
        MinerPosMap[int.Parse(NowPos) - 1] = false;
        MinerPos[int.Parse(NextPos) - 1] = int.Parse(MinerCount);

        GameObject Pos = Position.transform.Find(NowPos).gameObject;
        Pos.GetComponent<BoxCollider2D>().enabled = true;
        Pos.GetComponent<DropParent>().SetMinerNumber();

        GameObject PosNext = Position.transform.Find(NextPos).gameObject;
        PosNext.GetComponent<BoxCollider2D>().enabled = false;

        string prepabId = "Prefabs/Miner" + MinerCount;
        GameObject MinerObjPrefab = Resources.Load(prepabId) as GameObject;
        GameObject MinerObj = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        MinerObj.name = MinerCount.ToString();
        MinerObj.transform.SetParent(PosNext.transform);
        MinerObj.transform.SetAsFirstSibling();
        //MinerObj.transform.localPosition = new Vector2(0, 0);
        MinerObj.transform.localPosition = minerPosVector[int.Parse(MinerCount) -1];
        Pos.GetComponent<DropParent>().DisableNumber();
        PosNext.GetComponent<DropParent>().SetMinerNumber();
    }
    public string ChangeFormat(double target)
    {
        string haveGold = target.ToString("0");
        if (double.IsInfinity(target) == true)
            return "infinity";
        string[] unit = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L","M","N","O","P","Q","R","S","T","U",
        "V","W","X","Y","Z","Aa","Ab","Ac","Ad","Ae","Af","Ag","Ah","Ai","Aj","Ak","Al","Am","An","Ao","Ap","Aq","Ar","As","At","Au","Av","Aw","Ax","Ay","Az",
        "Ba","Bb","Bc","Bd","Be","Bf","Bg","Bh","Bi","Bj","Bk","Bl","Bm","Bn","Bo","Bp","Bq","Br","Bs","Bt","Bu","Bv","Bw","Bx","By","Bz",
        "Ca","Cb","Cc","Cd","Ce","Cf","Cg","Ch","Ci","Cj","Ck","Cl","Cm","Cn","Co","Cp","Cq","Cr","Cs","Ct","Cu","Cv","Cw","Cx","Cy","Cz",
        "Da","Db","Dc","Dd","De","Df","Dg","Dh","Di","Dj","Dk","Dl","Dm","Dn","Do","Dp","Dq","Dr","Ds","Dt","Du","Dv","Dw","Dx","Dy","Dz",
        "Ea","Eb","Ec","Ed","Ee","Ef","Eg","Eh","Ei","Ej","Ek","El","Em","En","Eo","Ep","Eq","Er","Es","Et","Eu","Ev","Ew","Ex","Ey","Ez"};


        int[] cVal = new int[unit.Length];
        int index = 0;
        while (true)
        {
            string last4 = "";
            if (haveGold.Length >= 4)
            {
                last4 = haveGold.Substring(haveGold.Length - 4);
                int intLast4 = int.Parse(last4);

                cVal[index] = intLast4 % 1000;

                haveGold = haveGold.Remove(haveGold.Length - 3);
            }
            else
            {
                cVal[index] = int.Parse(haveGold);
                break;
            }

            index++;
        }

        if (index > 0)
        {
            int r = cVal[index] * 1000 + cVal[index - 1];
            string temp = (r / 1000f).ToString();

            //return string.Format("{0:#.###} {1}", (float)r / 1000f, unit[index]);                        
            return string.Format("{0} {1}", temp, unit[index]);
        }

        return haveGold;
    }

    public int[] GetRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;
        for (int i = 0; i < length; ++i)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max);
                isSame = false;
                for (int j = 0; j < i; ++j)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArray;
    }
}
