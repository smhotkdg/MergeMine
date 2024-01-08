using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MineAdventureUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EventUI;
    public GameObject LevelUpPanel;

    public GameObject RewardGold;
    public GameObject RewardStarCoin;
    public GameObject RewardGem;
    public GameObject RewardNut;

    public GameObject CompleteButton;
    public GameObject GoMineButton;

    public GameObject GameOverPanel;

    public List<GameObject> DrillObjectList;
    public GameObject SelectItemPanel;
    public List<GameObject> DrillPopUpList;
    public GameObject BGDefaultImage;
    public GameObject GamePanel;

    public Button StartMineButton;
    public Image PopUpfillImage;
    public Text PopUpPercentText;
    public Text PopupOwnPartText;
    public GameObject FillImageUnLock;

    public GameObject PowerLvUp;
    public GameObject RangeLvUp;
    void Start()
    {
        SetInitAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetView()
    {
        
        for (int i = 0; i < DrillObjectList.Count; i++)
        {
            int nutcount = (i * 5) + 10;
            DrillObjectList[i].transform.Find("DrillImage/Fill").gameObject.GetComponent<Image>().fillAmount = (float)(GameManager.Instance.DrillPartList[i] / (float)nutcount);
            float randSpeed = Random.Range(0.2f, 0.7f);
            DrillObjectList[i].transform.Find("Mask/DrillBG").gameObject.GetComponent<Animator>().speed = randSpeed;
            if (GameManager.Instance.DrillAdventureCount >= i)
            {
                DrillObjectList[i].transform.Find("Lock").gameObject.SetActive(false);
                DrillObjectList[i].transform.Find("DrillImage/Fill").gameObject.GetComponent<Image>().fillAmount = (float)(GameManager.Instance.DrillPartList[i] / (float)nutcount);
                FillImageUnLock.SetActive(false);
            }
            else
            {
                DrillObjectList[i].transform.Find("Lock").gameObject.SetActive(true);
            }

            int checkIndex = bCheck(i);
            if(checkIndex ==1)
            {
                DrillObjectList[i].transform.Find("Hand").gameObject.SetActive(true);
            }
            else
            {
                DrillObjectList[i].transform.Find("Hand").gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        SetView();
    }
    void SetInitAmount()
    {
       double defaultBuff = 10;
       for(int i=0; i< DrillObjectList.Count; i++)
       {
            defaultBuff = 10;
            for (int k = 0; k < i; k++)
            {
                defaultBuff = defaultBuff*10;
            }

            DrillObjectList[i].transform.Find("TextGoldAmount").GetComponent<Text>().text = "x " + GameManager.Instance.ChangeFormat(defaultBuff);
       }

    }
    public int bCheck(int index)
    {
        int CheckIndex = 0;
        int nutcount = (index * 5) + 10;
        if (GameManager.Instance.DrillPartList[index] >= nutcount)
        {         
            CheckIndex = 1;
        }
        else
        {
            CheckIndex = -1;
        }

        if (GameManager.Instance.DrillAdventureCount > index)
        {
          
            CheckIndex = 2;
        }
  
        return CheckIndex;
    }
    public void SelectItem(int index)
    {
        if(GameManager.Instance.DrillAdventureCount >= index)
        {
            if(GameManager.Instance.totalGem >=2)
            {
                StartMineButton.interactable = true;
            }
            else
            {
                StartMineButton.interactable = false;
            }
            int nutcount = (GameManager.Instance.DrillAdventureCount * 5) + 10;
            float percentf = (float)(GameManager.Instance.DrillPartList[index] / (float)nutcount);
            if (percentf > 1)
                percentf = 1;
            PopUpPercentText.text = (percentf * 100).ToString("N0") + " %";
            
            PopupOwnPartText.text = GameManager.Instance.DrillPartList[index] + " / " + nutcount;
            PopUpfillImage.fillAmount = percentf;


            if(GameManager.Instance.DrillPartList[index] >= nutcount)
            {
                CompleteButton.SetActive(true);
                GoMineButton.SetActive(false);
            }
            else
            {
                CompleteButton.SetActive(false);
                GoMineButton.SetActive(true);
            }

            if(GameManager.Instance.DrillAdventureCount > index)
            {
                CompleteButton.SetActive(false);
                GoMineButton.SetActive(false);
                SelectItemPanel.transform.Find("BG/DrillAdventureBG/under_construction").gameObject.SetActive(false);
                DrillPopUpList[index].GetComponent<Animator>().speed = 1;
                FillImageUnLock.SetActive(false);
            }
            else
            {
                SelectItemPanel.transform.Find("BG/DrillAdventureBG/under_construction").gameObject.SetActive(true);
                DrillPopUpList[index].GetComponent<Animator>().speed = 0;
                FillImageUnLock.SetActive(true);
            }

            SelectItemPanel.SetActive(true);
       
            for (int i = 0; i < DrillPopUpList.Count; i++)
            {
                if (DrillPopUpList[i].activeSelf == true)
                    DrillPopUpList[i].SetActive(false);
            }
            DrillPopUpList[index].SetActive(true);
        }
   
    }
    public void CraftDrill()
    {
        for (int k = 0; k < GameManager.Instance.GainPerSecMiner.Count; k++)
        {
            GameManager.Instance.GainPerSecMiner[k] = GameManager.Instance.GainPerSecMiner[k] * GameManager.Instance.MinerBuffPower[GameManager.Instance.DrillAdventureCount];
        }
        CompleteButton.SetActive(false);
        FillImageUnLock.SetActive(false);
        SelectItemPanel.transform.Find("BG/DrillAdventureBG/under_construction").gameObject.SetActive(false);
        DrillPopUpList[GameManager.Instance.DrillAdventureCount].GetComponent<Animator>().speed = 1;
        GameManager.Instance.DrillAdventureCount++;
        //드릴 업그레이드 이펙트
        SetView();
        UIManager.Instance.SetBuffPower();
    }
    public void disableSelectItemPanel()
    {
        UIManager.Instance.SetTotalStarCoinText();
        UIManager.Instance.SetTotalGemText();
        SelectItemPanel.SetActive(false);
    }

    public void GameStart()
    {
        EventUI.SetActive(true);
    }
    public void StartGameUI()
    {
        GameManager.Instance.totalGem -= 2;
        UIManager.Instance.SetTotalGemText();

        GamePanel.SetActive(true);
        GamePanel.GetComponent<MineAdventureGame>().OnEndGame += MineAdventureUIManager_OnEndGame;
        GamePanel.GetComponent<MineAdventureGame>().OnEndGameCount += MineAdventureUIManager_OnEndGameCount;
        BGDefaultImage.SetActive(false);
        SelectItemPanel.SetActive(false);
        SoundsManager.Instance.SetMiningAdventureBGM(true);
    }
    double Gold = 0;
    
    private void MineAdventureUIManager_OnEndGameCount(int isGold, int isStarCoin, int isGme, int isNut)
    {
        //골드는 수정해야함
        Gold = GameManager.Instance.GetNowGoldPerSec() * 60 * isGold;

        RewardGold.transform.Find("TextCount").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(Gold);
        
        RewardStarCoin.transform.Find("TextCount").GetComponent<Text>().text = isStarCoin.ToString();
        RewardGem.transform.Find("TextCount").GetComponent<Text>().text = isGme.ToString();
        RewardNut.transform.Find("TextCount").GetComponent<Text>().text = isNut.ToString();
 
    }
    bool bStartcoin = false;
    bool bGem =false;


    private void MineAdventureUIManager_OnEndGame(bool isGold, bool isStarCoin, bool isGme, bool isNut)
    {
        RewardGold.SetActive(false);
        RewardStarCoin.SetActive(false);
        RewardGem.SetActive(false);
        RewardNut.SetActive(false);
        if (isGold == true)
        {
            RewardGold.SetActive(true);
        }
        if(isStarCoin ==true)
        {
            RewardStarCoin.SetActive(true);
            bStartcoin = true;
        }
        if(isGme ==true)
        {
            RewardGem.SetActive(true);
            bGem = true;
        }
        if(isNut ==true)
        {
            RewardNut.SetActive(true);
        }
    }

    public void GameEnd()
    {
        BGDefaultImage.SetActive(true);
        GamePanel.SetActive(false);
        SetView();
        if(Gold >0)
        {
            GameManager.Instance.totalMoney += Gold;
            Gold = 0;
            UIManager.Instance.SetMoney();
            CollectionParticleManager.Instance.StartCoinParticle(10);
        }
        if(bStartcoin == true)
        {
            CollectionParticleManager.Instance.StartStarCoinParticle(5);
            bStartcoin = false;
        }
        if (bGem == true)
        {
            CollectionParticleManager.Instance.StartGemParticle(5);
            bGem = false;
        }

        UIManager.Instance.SetTotalStarCoinText();
        UIManager.Instance.SetTotalGemText();
        //할까말까
        //SelectItemPanel.SetActive(true);'
    }
    public void SetGameOverPanel(bool flag)
    {
        GameOverPanel.SetActive(flag);
        if(flag == false)
        {
            SoundsManager.Instance.SetMiningAdventureBGM(false);
            GameEnd();
        }
    }
    public void SetLevelUpPanel(bool flag)
    {
        LevelUpPanel.SetActive(flag);
        if(flag ==true)
        {
            InitLvUpPanel();
        }
    }
    void InitLvUpPanel()
    {
        if(GameManager.Instance.totalStarCoin >= (GameManager.Instance.DrillAdventureTotalDigCount - 6) * 2)
        {
            PowerLvUp.transform.Find("ButtonUpgrade").gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            PowerLvUp.transform.Find("ButtonUpgrade").gameObject.GetComponent<Button>().interactable = false;
        }
        if (GameManager.Instance.totalStarCoin >= GameManager.Instance.DrillAdventureTotalDigRangeCount*10)
        {
            RangeLvUp.transform.Find("ButtonUpgrade").gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            RangeLvUp.transform.Find("ButtonUpgrade").gameObject.GetComponent<Button>().interactable = false;
        }
        PowerLvUp.transform.Find("ButtonUpgrade/TextCost").gameObject.GetComponent<Text>().text = ((GameManager.Instance.DrillAdventureTotalDigCount - 6) * 2).ToString();
        RangeLvUp.transform.Find("ButtonUpgrade/TextCost").gameObject.GetComponent<Text>().text = (GameManager.Instance.DrillAdventureTotalDigRangeCount * 10).ToString();
        PowerLvUp.transform.Find("TextCount").gameObject.GetComponent<Text>().text = GameManager.Instance.DrillAdventureTotalDigCount.ToString();
        RangeLvUp.transform.Find("TextCount").gameObject.GetComponent<Text>().text = GameManager.Instance.DrillAdventureTotalDigRangeCount.ToString();
    }
    public void UpgradeMiningCount()
    {
        GameManager.Instance.totalStarCoin -= (GameManager.Instance.DrillAdventureTotalDigCount-6)*2;
        UIManager.Instance.SetTotalStarCoinText();
        GameManager.Instance.DrillAdventureTotalDigCount++;
        InitLvUpPanel();
    }
    public void UpgradeMiningRange()
    {
        GameManager.Instance.totalStarCoin -= GameManager.Instance.DrillAdventureTotalDigRangeCount *10;
        UIManager.Instance.SetTotalStarCoinText();
        GameManager.Instance.DrillAdventureTotalDigRangeCount++;
        InitLvUpPanel();
    }
}
