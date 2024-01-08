using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject AdsGold;
    public GameObject AdsSpeed;

    public Text BuffPowerText;
    public GameObject DrillAdventureObj;
    public GameObject ShopObj;
    public List<GameObject> PosLockList;
    public GameObject ShopPanelObj;
    public GameObject MineEffect;
    public GameObject MineAdventureObj;
    public GameObject DrillObj;
    public GameObject SpeedUpObj;
    public Text SpeedUPText;
    public Text SpeedUPTimeText;
    public GameObject SpinBuffObj;
    public Text SpinBuffText;
    public Text SpinTimeText;
    public GameObject TrainExclamationMark;
    public GameObject ExitPanel;
    public GameObject TrainObj;
    public GameObject SpinIconObj;
    public GameObject GoldRushPanel;
    public GameObject SpinObj;
    public GameObject SpinPanel;
    public GameObject NewMinerBuyPanel;
    public GameObject AdsRewardPanel;
    public GameObject TimeRewardPanel;
    public GameObject SettingPanel;
    public GameObject MinerCollectionPanel;
    public GameObject BoxRewardPanel;
    public GameObject NewMinerPanel;
    public Text TotalMoney;
    public Text totalGem;
    public Text totalStarCoion;

    public List<GameObject> PrevMinerList;
    public List<GameObject> NextMinerList;
    public Text LvupText;
    public Text LvDownText;
    public GameObject PreObj;
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton UIManager == null");
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
    }
    public void SetBuffPower()
    {
        double temp =1;
        if(GameManager.Instance.DrillAdventureCount ==0)
        {
            BuffPowerText.text = "-- %";
        }
        else
        {
            for (int i = 0; i < GameManager.Instance.DrillAdventureCount; i++)
            {
                temp = GameManager.Instance.MinerBuffPower[i] * temp;
            }

            BuffPowerText.text = GameManager.Instance.ChangeFormat(temp) + " %";
        }
       
    }
    public void SetPosLock()
    {
        bool isInit = false;
        for(int i=0;i<GameManager.Instance.posLock.Length;i++)
        {
            if(GameManager.Instance.posLock[i] ==0)
            {
                PosLockList[i].SetActive(false);
            }
            else
            {
                if(isInit == false)
                {
                    PosLockList[i].transform.Find("Gold").gameObject.SetActive(true);
                    PosLockList[i].GetComponent<Button>().interactable = true;
                    isInit = true;
                }
            }
            
        }
    }
    void BuyLock(int index,int Cost,bool isEnd = false)
    {
        if (GameManager.Instance.totalStarCoin >= Cost)
        {
            PosLockList[index].SetActive(false);
            if (isEnd == false)
            {
                if(PosLockList.Count != index + 1)
                {
                    PosLockList[index + 1].GetComponent<Button>().interactable = true;
                    PosLockList[index + 1].SetActive(true);
                    PosLockList[index + 1].transform.Find("Gold").gameObject.SetActive(true);
                }     
            }
            GameManager.Instance.totalStarCoin -= Cost;
            SetTotalStarCoinText();
            GameManager.Instance.posLock[index] = 0;
            GameManager.Instance.MinerPos[index+6] = 0;
            GameManager.Instance.Position.transform.GetChild(index+6).GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            return;
        }
    }
    public void BuyPosLock(int index)
    {
        switch (index)
        {
            case 0:
                BuyLock(index,2, false);
                //2
                break;
            case 1:
                //10
                BuyLock(index, 10, false);
                break;
            case 2:
                //20
                BuyLock(index, 20, false);
                break;
            case 3:
                //30
                BuyLock(index, 30, false);
                break;
            case 4:
                //40
                BuyLock(index, 40, false);
                break;
            case 5:
                //50
                BuyLock(index, 50, false);
                break;
        }

    }
    public void SetBuffTimeText(bool flag)
    {
        if(SpinBuffObj.activeSelf == !flag)
        {
            if(flag == true && GameManager.Instance.AdsGoldPower >1)
            {
                SpinBuffObj.SetActive(true);
            }                
            else
            {
                SpinBuffObj.SetActive(false);
            }
        }
    }
    public void SetSpeedUpText(bool flag)
    {
        if (SpeedUpObj.activeSelf == !flag)
        {
            if (flag == true && GameManager.Instance.bStartAdsSpeedUp == true)
            {
                SpeedUpObj.SetActive(true);
            }
            else
            {
                SpeedUpObj.SetActive(false);
            }
        }
    }
    public void SetSpinBuff()
    {
        if(SpinBuffObj.activeSelf ==true)
        {
            SpinBuffText.text = "x " + GameManager.Instance.AdsGoldPower;
            SpinTimeText.text = TimerManager.Instance.FloatToTime(GameManager.Instance.SpinTime, "#00:00");
        }
    }
    public void SetSpeedBuff()
    {
        if (SpeedUpObj.activeSelf == true)
        {
            GameManager.Instance.SpeedTime -= Time.deltaTime;
            SpeedUPTimeText.text = TimerManager.Instance.FloatToTime(GameManager.Instance.SpeedTime, "#00:00");
        }
    }
    public void SetTrainExclamationMark(bool flag)
    {
        if(TrainExclamationMark.activeSelf ==!flag)
        {
            TrainExclamationMark.SetActive(flag);
        }        
    }
    public void SetSpinAnim(bool flag)
    {
        SpinObj.GetComponent<Animator>().SetBool("isSpin", flag);
        SetBuffTimeText(!flag);
    }
    public void LvUp()
    {
        LvupText.text = (GameManager.Instance.GetminerLevel() + 1).ToString() ;
    }
    public void SetIcon()
    {
        SpinIconObj.SetActive(GameManager.Instance.bEndSpinTutorial);
        TrainObj.SetActive(GameManager.Instance.bEndTrainTutorial);
        DrillAdventureObj.SetActive(GameManager.Instance.bEndDrillAdventureTutorial);
        ShopObj.SetActive(GameManager.Instance.bEndShopTutorial);


        AdsGold.SetActive(GameManager.Instance.bEndAdsGold);
        AdsSpeed.SetActive(GameManager.Instance.BEndAdsSpeed);


        if (GameManager.Instance.Istutorial == true)
        {
            SpinIconObj.SetActive(true);
            TrainObj.SetActive(true);
            DrillAdventureObj.SetActive(true);
            ShopObj.SetActive(true);


            AdsGold.SetActive(true);
            AdsSpeed.SetActive(true);


            GameManager.Instance.bEndTrainTutorial = true;
            GameManager.Instance.bEndSpinTutorial = true;

            GameManager.Instance.bEndDrillAdventureTutorial = true;
            GameManager.Instance.bEndShopTutorial = true;


        }
    }
    public void SetMinerView()
    {
        LvDown();
        LvUp();
        for (int i = 0; i < NextMinerList.Count; i++)
        {
            NextMinerList[i].SetActive(false);
        }
        NextMinerList[GameManager.Instance.GetminerLevel()].SetActive(true);

        for (int i = 0; i < PrevMinerList.Count; i++)
        {
            PrevMinerList[i].SetActive(false);
        }
        bool isPrev = false;
        if (GameManager.Instance.GetminerLevel() - 1 >= 1)
        {
            PrevMinerList[GameManager.Instance.GetminerLevel() - 2].SetActive(true);
            isPrev = true;
        }   
        PreObj.SetActive(isPrev);   
    }
    public void SetMinerColor(bool flag)
    {
        for (int i = 0; i < NextMinerList.Count; i++)
        {
            if(NextMinerList[i].activeSelf ==true)
            {
                Color myColor;
                myColor = NextMinerList[i].GetComponent<Image>().color;
                if (flag == true)
                {
                    myColor.a = 1f;
                    NextMinerList[i].GetComponent<Image>().color = myColor;
                }
                
                else{
                    myColor.a = 0.5f;
                    NextMinerList[i].GetComponent<Image>().color = myColor;
                }
            }
            
        }
        
    }
    public void LvDown()
    {
        LvDownText.text = (GameManager.Instance.GetminerLevel()-1).ToString();
   
    }
    void Start()
    {
        if(GameManager.Instance.isSpin ==true)
        {
            SetSpinAnim(false);
        }
        else
        {
            SetSpinAnim(true);
        }       
   
    }
    public void SetMoney()
    {
        TotalMoney.text = GameManager.Instance.ChangeFormat(GameManager.Instance.GetTotalMoney());
    }
    public void SetTotalGemText()
    {
        totalGem.text = GameManager.Instance.ChangeFormat(GameManager.Instance.totalGem);
    }
    public void SetTotalStarCoinText()
    {
        totalStarCoion.text = GameManager.Instance.ChangeFormat(GameManager.Instance.totalStarCoin);
    }
    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPanel();
        }
        SetSpinBuff();
        SetSpeedBuff();
    }
    void CheckPanel()
    {
        if(BoxRewardPanel.activeSelf ==true)
        {
            SetBoxRewardPanel(false);
            return;
        }
        if (NewMinerPanel.activeSelf == true)
        {
            SetNewMinerPanel(false);
            return;
        }
        if (MinerCollectionPanel.activeSelf == true)
        {
            SetinerCollectionPanel(false);
            return;
        }
        if(SettingPanel.activeSelf ==true)
        {
            SetSettingPanel(false);
            return;            
        }      
        if(AdsRewardPanel.activeSelf == true)
        {
            SetAdsRewardPanel(false);
            return;
        }
        if(NewMinerBuyPanel.activeSelf ==true)
        {
            SetNewMinerBuyPanel(false);
            return;
        }
        if(SpinPanel.activeSelf ==true)
        {
            SetSpinPanel(false);
            return;
        }
        if(GoldRushPanel.activeSelf ==true)
        {
            SetGoldRushPanel(false);
            return;
        }
        if(ExitPanel.activeSelf ==true)
        {
            ExitPanel.SetActive(false);
            return;
        }
        if(DrillObj.activeSelf==true)
        {
            SstDrillObj(false);
            return;
        }
        ExitPanel.SetActive(true);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void SetExitPanel(bool flag)
    {
        ExitPanel.SetActive(flag);
    }
    public void SetTotalMoney(double money)
    {
        if (money > 0)
            GameManager.Instance.AddTotalMoney(money);
        else
            GameManager.Instance.SubtractTotalMoney(money);

        TotalMoney.text = GameManager.Instance.ChangeFormat(GameManager.Instance.GetTotalMoney());
    }
    public void SetBoxRewardPanel(bool flag)
    {
        BoxRewardPanel.SetActive(flag);
        GameManager.Instance.SetMInerCollider(!flag);
        if(flag ==true)
        {
            BoxRewardPanel.GetComponent<BoxRewardUIManager>().SetBoxReward();
        }
        GameManager.Instance.SetMInerCollider(!flag);
    }

    public void SetBoxRewardPanel(int index)
    {
        BoxRewardPanel.SetActive(true);
        GameManager.Instance.SetMInerCollider(false);        
        BoxRewardPanel.GetComponent<BoxRewardUIManager>().SetBoxReward(index);
    }
    public void SetNewMinerPanel(bool flag)
    {
        NewMinerPanel.SetActive(flag);
        if (flag == true)
        {
           
            NewMinerPanel.GetComponent<GetNewMinerPopupManager>().SetMinerData(GameManager.Instance.MaxMergetNumber);            
        }
        else
        {
            if (GameManager.Instance.TutorialIndex == 2)
            {
                TutorialManager.Instance.SetEndNewMinerPanel();                
            }
            UIManager.Instance.SetNewMinerBuyPanel(true);
            if(GameManager.Instance.MaxMergetNumber ==4)
            {
                TutorialManager.Instance.StartTutorialSpin1();
            }
            if (GameManager.Instance.MaxMergetNumber == 6)
            {
                TutorialManager.Instance.StartTrainTutorial_1();
            }
            if (GameManager.Instance.MaxMergetNumber == 3)
            {
                TutorialManager.Instance.StartAdsSpeedUP();
            }
            if (GameManager.Instance.MaxMergetNumber == 5)
            {
                TutorialManager.Instance.StartAdsGold();
            }
        }
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetinerCollectionPanel(bool flag)
    {
        MinerCollectionPanel.SetActive(flag);
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetSettingPanel(bool flag)
    {
        SettingPanel.SetActive(flag);
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetTimeRewardPanel(bool flag,float time =0)
    {
        TimeRewardPanel.SetActive(flag);
        if(flag == true)
        {
            TimeRewardPanel.GetComponent<TimeRewardPanelManager>().InitData(time);
            SoundsManager.Instance.RewardSound();
        }
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetAdsRewardPanel(bool flag)
    {
        AdsRewardPanel.SetActive(flag);
        if(flag ==false && GameManager.Instance.bStartAdsSpeedUp == true)
        {
            GameManager.Instance.SetSpeedUP();
        }
        if(flag ==true)
            SoundsManager.Instance.RewardSound();

        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetNewMinerBuyPanel(bool flag)
    {
        if(flag ==true)
        {
            if (GameManager.Instance.MaxMergetNumber - 5 > GameManager.Instance.minerNumber)
            {
                NewMinerBuyPanel.SetActive(flag);
                NewMinerBuyPanel.GetComponent<NewMinerBuyPanelManager>().SetIInitMiner();
            }
        }            
        else
        {
            NewMinerBuyPanel.SetActive(flag);
            if (GameManager.Instance.TutorialIndex == 55)
            {
                if (GameManager.Instance.MaxMergetNumber - 5 > GameManager.Instance.minerNumber)
                {
                    TutorialManager.Instance.SetGetNextMIner();
                }
            }
        
            if (GameManager.Instance.MaxMergetNumber == 8)
            {
                TutorialManager.Instance.StartDrillTutorial();
            }
        }
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetSpin()
    {
        if(SpinPanel.activeSelf ==true)
        {
            SpinPanel.GetComponent<SpinWheelManager>().SucessAds();
        }

    }
    public void SetSpinPanel(bool flag)
    {
        SpinPanel.SetActive(flag);
        if(flag == false)
            SpinIconObj.SetActive(GameManager.Instance.bEndSpinTutorial);
        GameManager.Instance.SetMInerCollider(!flag);
    }

    public void SetGoldRushPanel(bool flag)
    {
        GoldRushPanel.SetActive(flag);
        if(flag ==false)
            TrainObj.SetActive(GameManager.Instance.bEndTrainTutorial);
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void GoldRushStart()
    {
        if(GoldRushPanel.activeSelf ==true)
        {
            GoldRushPanel.GetComponent<GoldrushManager>().CompleteAds();
        }
    }
    public void SstDrillObj(bool flag)
    {
        DrillObj.SetActive(flag);
        if(flag == true)
        {
            GameManager.Instance.DrillUIInit();
            CollectionParticleManager.Instance.StartGemParticle(20);
            CollectionParticleManager.Instance.StartStarCoinParticle(20);
        }
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetMineAdventure(bool flag)
    {
        MineAdventureObj.SetActive(flag);
        MineEffect.SetActive(!flag);
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetGameOverDrillAdventure()
    {
        if(MineAdventureObj.activeSelf ==true)
        {
            MineAdventureObj.GetComponent<MineAdventureUIManager>().SetGameOverPanel(true);
        }
    }
    public void DrillGameStart()
    {
        if (MineAdventureObj.activeSelf == true)
        {
            MineAdventureObj.GetComponent<MineAdventureUIManager>().StartGameUI();
        }
    }
    public void SetShopPanelObj(bool flag)
    {
        ShopPanelObj.SetActive(flag);
        GameManager.Instance.SetMInerCollider(!flag);
    }
    public void SetBuyComplete(string name)
    {
        if(ShopPanelObj.activeSelf ==true)
        {
            ShopPanelObj.GetComponent<ShopPanelUIManager>().SetSelectType(name);
            ShopPanelObj.GetComponent<ShopPanelUIManager>().SetBuyCompletePanel(true);
        }
    }
}
