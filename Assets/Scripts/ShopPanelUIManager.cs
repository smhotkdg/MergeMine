using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;
public class ShopPanelUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button FreeGoldBar;
    public GameObject BuyCompletePanel;

    public GameObject CompleteNoAds;
    public GameObject CompleteGoldBar1;
    public GameObject CompleteGoldBar2;
    public GameObject CompleteGoldBar3;

    public GameObject CompleteTimeTicket1;
    public GameObject CompleteTimeTicket2;
    public GameObject CompleteTimeTicket3;

    public GameObject CompleteKey1;
    public GameObject CompleteKey2;
    public GameObject CompleteKey3;



    public GameObject BuyComfirmPanel;
    public GameObject TimeTicket1;
    public GameObject TimeTicket2;
    public GameObject TimeTicket3;

    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;

    public Text TimeText_1;
    public Text TimeText_2;
    public Text TimeText_3;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        double TextMoney = GameManager.Instance.GetNowGoldPerSec();
        TimeText_1.text = GameManager.Instance.ChangeFormat(TextMoney * 3600);
        TimeText_2.text = GameManager.Instance.ChangeFormat(TextMoney * 21600);
        TimeText_3.text = GameManager.Instance.ChangeFormat(TextMoney * 63200);        
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.FreeGoldBarTime <= 0)
        {
            SetFreeButton(true);
            FreeGoldBar.transform.Find("Text").GetComponent<Text>().text = "Free";
        }
        else
        {            
            FreeGoldBar.transform.Find("Text").GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.FreeGoldBarTime, "#00:00");
            SetFreeButton(false);
        }
    }
    void SetFreeButton(bool flag)
    {
        FreeGoldBar.interactable = flag;
    }
    double TimeMoney = 0;
    int buyIndex = -1;
    public void BuyTicket(int index)
    {
        TimeMoney = GameManager.Instance.GetNowGoldPerSec();
        SetBuyComfirmPanel(true);
        
        switch (index)
        {
            case 0:
                TimeMoney = TimeMoney * 3600;
                SetBuyFomfirmUI("Ticket1");
                break;
            case 1:
                TimeMoney = TimeMoney * 21600;
                SetBuyFomfirmUI("Ticket2");
                break;
            case 2:
                TimeMoney = TimeMoney * 63200;
                SetBuyFomfirmUI("Ticket3");
                break;
        }
        buyIndex = index;
    }
    string Selecttype;
    public void SetSelectType(string Name)
    {
        Selecttype = Name;
    }
    public void SetBuyFomfirmUI(string type)
    {
        TimeTicket1.SetActive(false);
        TimeTicket2.SetActive(false);
        TimeTicket3.SetActive(false);
        Key1.SetActive(false);
        Key2.SetActive(false);
        Key3.SetActive(false);
        Selecttype = type;
        switch (type)
        {
            case "Ticket1":
                TimeTicket1.SetActive(true);
                TimeTicket1.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                break;
            case "Ticket2":
                TimeTicket2.SetActive(true);
                TimeTicket2.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                break;
            case "Ticket3":
                TimeTicket3.SetActive(true);
                TimeTicket3.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                break;

            case "Key1":
                Key1.SetActive(true);
                break;
            case "Key2":
                Key2.SetActive(true);
                break;
            case "Key3":
                Key3.SetActive(true);
                break;
        }
    }

    public void buyComfirmOK()
    {       

        switch (Selecttype)
        {
            case "Ticket1":
                if(GameManager.Instance.totalStarCoin >=15)
                {
                    GameManager.Instance.totalStarCoin -= 15;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Ticket2":
                if (GameManager.Instance.totalStarCoin >= 80)
                {
                    GameManager.Instance.totalStarCoin -= 80;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Ticket3":
                if (GameManager.Instance.totalStarCoin >= 140)
                {
                    GameManager.Instance.totalStarCoin -= 140;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Key1":
                if (GameManager.Instance.totalStarCoin >= 10)
                {
                    GameManager.Instance.totalStarCoin -= 10;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Key2":
                if (GameManager.Instance.totalStarCoin >= 20)
                {
                    GameManager.Instance.totalStarCoin -= 20;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Key3":
                if (GameManager.Instance.totalStarCoin >= 20)
                {
                    GameManager.Instance.totalStarCoin -= 20;
                    UIManager.Instance.SetTotalStarCoinText();
                    SetBuyComfirmPanel(false);
                    SetBuyCompletePanel(true);
                }
                else
                {
                    return;
                }
                break;
            case "Noads":
                SetBuyComfirmPanel(false);
                SetBuyCompletePanel(true);
                break;

            case "GoldBar1":
                SetBuyComfirmPanel(false);
                SetBuyCompletePanel(true);
                break;
            case "GoldBar2":
                SetBuyComfirmPanel(false);
                SetBuyCompletePanel(true);
                break;
            case "GoldBar3":
                SetBuyComfirmPanel(false);
                SetBuyCompletePanel(true);
                break;
        }  
    }

    public void BuyKey(int index)
    {        
        SetBuyComfirmPanel(true);
        switch (index)
        {
            case 0:                
                SetBuyFomfirmUI("Key1");
                break;
            case 1:                
                SetBuyFomfirmUI("Key2");
                break;
            case 2:                
                SetBuyFomfirmUI("Key3");
                break;
        }
    
    }
    public void SetBuyCompletePanel(bool flag)
    {
        BuyCompletePanel.SetActive(flag);
        if(flag ==true)
        {
            CompleteTimeTicket1.SetActive(false);
            CompleteTimeTicket2.SetActive(false);
            CompleteTimeTicket3.SetActive(false);

            CompleteGoldBar1.SetActive(false);
            CompleteGoldBar2.SetActive(false);
            CompleteGoldBar3.SetActive(false);

            CompleteKey1.SetActive(false);
            CompleteKey2.SetActive(false);
            CompleteKey3.SetActive(false);
            CompleteNoAds.SetActive(false);

            switch (Selecttype)
            {
                case "Ticket1":
                    CompleteTimeTicket1.SetActive(true);
                    CompleteTimeTicket1.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                    GameManager.Instance.totalMoney += TimeMoney;
                    UIManager.Instance.SetMoney();
                    CollectionParticleManager.Instance.StartCoinParticle(10);
                    break;
                case "Ticket2":
                    CompleteTimeTicket2.SetActive(true);
                    CompleteTimeTicket2.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                    CollectionParticleManager.Instance.StartCoinParticle(20);
                    GameManager.Instance.totalMoney += TimeMoney;
                    UIManager.Instance.SetMoney();
                    break;
                case "Ticket3":
                    CompleteTimeTicket3.SetActive(true);
                    CompleteTimeTicket3.transform.Find("Text").GetComponent<Text>().text = GameManager.Instance.ChangeFormat(TimeMoney);
                    CollectionParticleManager.Instance.StartCoinParticle(30);
                    GameManager.Instance.totalMoney += TimeMoney;
                    UIManager.Instance.SetMoney();
                    break;
                case "Key1":
                    CompleteKey1.SetActive(true);
                    CollectionParticleManager.Instance.StartGemParticle(5);
                    GameManager.Instance.totalGem += 20;
                    UIManager.Instance.SetTotalGemText();
                    break;
                case "Key2":
                    CompleteKey2.SetActive(true);
                    CollectionParticleManager.Instance.StartGemParticle(10);
                    GameManager.Instance.totalGem += 50;
                    UIManager.Instance.SetTotalGemText();
                    break;
                case "Key3":
                    CompleteKey3.SetActive(true);
                    CollectionParticleManager.Instance.StartGemParticle(15);
                    GameManager.Instance.totalGem += 150;
                    UIManager.Instance.SetTotalGemText();
                    break;
                case "Noads":
                    CompleteNoAds.SetActive(true);
                    CollectionParticleManager.Instance.StartGemParticle(10);
                    CollectionParticleManager.Instance.StartStarCoinParticle(10);
                    GameManager.Instance.totalGem += 100;
                    GameManager.Instance.totalStarCoin += 100;
                    UIManager.Instance.SetTotalGemText();
                    UIManager.Instance.SetTotalStarCoinText();
                    break;

                case "GoldBar1":
                    CompleteGoldBar1.SetActive(true);
                    CollectionParticleManager.Instance.StartStarCoinParticle(5);
                    GameManager.Instance.totalStarCoin += 30;                    
                    UIManager.Instance.SetTotalStarCoinText();
                    break;
                case "GoldBar2":
                    CompleteGoldBar2.SetActive(true);
                    CollectionParticleManager.Instance.StartStarCoinParticle(10);
                    GameManager.Instance.totalStarCoin += 85;
                    UIManager.Instance.SetTotalStarCoinText();
                    break;
                case "GoldBar3":
                    CompleteGoldBar3.SetActive(true);
                    CollectionParticleManager.Instance.StartStarCoinParticle(20);
                    GameManager.Instance.totalStarCoin += 470;
                    UIManager.Instance.SetTotalStarCoinText();
                    break;
            }
        }
    }
    public void SetBuyComfirmPanel(bool flag)
    {
        BuyComfirmPanel.SetActive(flag);          
    }

    public void GetFreeGoldBar()
    {
        FreeGoldBar.interactable = false;
        GameManager.Instance.FreeGoldBarTime = GameManager.Instance.DefaultFreeGoldBarTime;
        GameManager.Instance.totalStarCoin += 1;
        UIManager.Instance.SetTotalStarCoinText();
        CollectionParticleManager.Instance.StartStarCoinParticle(1);
    }
}
