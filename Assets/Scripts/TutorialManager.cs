using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject TouchTutorial;
    public GameObject AdsTutorialSpeed;
    public GameObject AdsTutorialSpeed_2;

    public GameObject AdsTutorialGold;
    public GameObject AdsTutorialGold_2;


    public GameObject ShopTutorial_1;
    public GameObject ShopTutorial_2;
    public GameObject ShopTutorial_3;

    public GameObject DrillTutorial_1;
    public GameObject DrillTutorial_2;
    public GameObject DrillTutorial_3;
    public GameObject DrillTutorial_3_1;
    public GameObject DrillTutorial_4;
    
    public GameObject DrillTutorial_5;

    public GameObject TutorialTrain1;
    public GameObject TutorialTrain2;

    public GameObject TutorialPanel;
    public GameObject Tutorial1;
    public GameObject Tutorial2;    
    public GameObject Tutorial3;
    public GameObject Tutorial4;
    public GameObject Tutorial5;

    public GameObject Tutorial_ads1;
    public GameObject Tutorial_ads2;
    public GameObject Tutorial_ads3;

    public GameObject TutorialSpin1;
    public GameObject TutorialSpin2;
    private static TutorialManager _instance = null;

    public static TutorialManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton TutorialManager == null");
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


    public void StartShopTutorial_1()
    {
        ShopTutorial_1.SetActive(true);
    }
    public void StartShopTutorial_2()
    {
        ShopTutorial_1.SetActive(false);
        ShopTutorial_2.SetActive(true);
    }
    public void StartShopTutorial_3()
    {
        ShopTutorial_2.SetActive(false);
        ShopTutorial_3.SetActive(true);

    }
    public void EndShopTurorial()
    {
        ShopTutorial_3.SetActive(false);
        GameManager.Instance.bEndShopTutorial = true;
        UIManager.Instance.SetIcon();
    }

    public void StartDrillTutorial()
    {
        DrillTutorial_1.SetActive(true);
    }
    public void StartDrillTutorial_2()
    {
        DrillTutorial_1.SetActive(false);
        DrillTutorial_2.SetActive(true);
    }
    public void StartDrillTutorial_3()
    {
        DrillTutorial_2.SetActive(false);
        DrillTutorial_3_1.SetActive(true);
    }

    public void StartDrillTutorial_3_1()
    {
        DrillTutorial_3_1.SetActive(false);
        DrillTutorial_3.SetActive(true);
    }
    public void StartDrillTutorial_4()
    {
        StartCoroutine(Tutorial4Rutine());
        DrillTutorial_3.SetActive(false);
    }
    IEnumerator Tutorial4Rutine()
    {
        yield return new WaitForSeconds(1.5f);        
        DrillTutorial_4.SetActive(true);
    }
    public void StartDrillTutorial_5()
    {
        DrillTutorial_4.SetActive(false);
        DrillTutorial_5.SetActive(true);
    }
    public void EndDrillTutorial()
    {
        DrillTutorial_5.SetActive(false);
        GameManager.Instance.bEndDrillAdventureTutorial = true;
        EndAllTurorial();
        UIManager.Instance.SetIcon();
    }

    public void StartTrainTutorial_1()
    {
        TutorialTrain1.SetActive(true);
    }
    public void StartTrainTutorial2()
    {
        TutorialTrain1.SetActive(false);
        TutorialTrain2.SetActive(true);
    }
    public void EndTrainTutorial()
    {
        TutorialTrain2.SetActive(false);
        GameManager.Instance.bEndTrainTutorial = true;
        UIManager.Instance.SetIcon();
    }

    


    public void StartTutorialSpin1()
    {
        TutorialSpin1.SetActive(true);
    }
    public void StartTutorialSpin2()
    {
        TutorialSpin2.SetActive(true);        
    }
    public void EndTutorialSpin_1()
    {
        TutorialSpin1.SetActive(false);
        StartTutorialSpin2();
    }
    public void EndTutorialSpin_2()
    {
        TutorialSpin2.SetActive(false);
        GameManager.Instance.bEndSpinTutorial = true;
        UIManager.Instance.SetIcon();

    }
    public void StartTutorial()
    {
        TutorialPanel.SetActive(true);
        SetNextTutorial();
    }
    public void SetNextTutorial()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(false);

        switch (GameManager.Instance.TutorialIndex)
        {
            case 0:
                Tutorial1.SetActive(true);
                break;
            case 1:
                Tutorial2.SetActive(true);
                break;
            case 2:
                Tutorial3.SetActive(true);
                break;
            case 3:
                Tutorial4.SetActive(true);
                break;          
          
        }
    }
    int nextIndex = 0;
    public void GetMiner()
    {
        nextIndex++;
        if(nextIndex ==2)
        {
            GameManager.Instance.TutorialIndex = 1;
            SetNextTutorial();            
        }
    }
    public void SetMergeMInerTutorial()
    {
        GameManager.Instance.TutorialIndex = 2;
        SetNextTutorial();
    }
    public void SetEndNewMinerPanel()
    {
        GameManager.Instance.TutorialIndex = 3;
        SetNextTutorial();
    }
    public void SetMoveMinerTutorial()
    {
        //GameManager.Instance.TutorialIndex = 55;
        GameManager.Instance.TutorialIndex = 4;
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(false);
        StartTouchTutorial();
    }

    void DisableTutorialAds()
    {
        Tutorial_ads1.SetActive(false);
        Tutorial_ads2.SetActive(false);
        Tutorial_ads3.SetActive(false);
    }
    public void SetAds1Tutorial()
    {
        DisableTutorialAds();
        if(GameManager.Instance.TutorialIndex ==4)
        {
            Tutorial_ads1.SetActive(true);
        }
    }
    public void EndAds1Tutorial()
    {
        //GameManager.Instance.TutorialIndex = 5;
        GameManager.Instance.TutorialIndex = 55;
        Tutorial_ads1.SetActive(false);
    }
    public void SetAds2Tutorial()
    {
        DisableTutorialAds();
        if (GameManager.Instance.TutorialIndex == 5)
        {
            Tutorial_ads2.SetActive(true);
        }
    }
    public void EndAds2Tutorial()
    {
        GameManager.Instance.TutorialIndex = 6;
        Tutorial_ads2.SetActive(false);
    }
    public void SetAds3Tutorial()
    {
        DisableTutorialAds();
        if (GameManager.Instance.TutorialIndex == 6)
        {
            Tutorial_ads3.SetActive(true);
        }
    }
    public void EndAds3Tutorial()
    {
        GameManager.Instance.TutorialIndex = 55;
        Tutorial_ads3.SetActive(false);
        UIManager.Instance.SetIcon();
    }

    public void SetGetNextMIner()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
        Tutorial3.SetActive(false);
        Tutorial4.SetActive(false);
        Tutorial5.SetActive(false);
        Tutorial5.SetActive(true);                
    }
    public void EndTutorial()
    {
        Tutorial5.SetActive(false);
        TutorialManager.Instance.StartShopTutorial_1();
        GameManager.Instance.TutorialIndex = 1000;
        UIManager.Instance.SetIcon();
    }

    public void EndAllTurorial()
    {
        GameManager.Instance.Istutorial = true;
        TutorialPanel.SetActive(false);
        UIManager.Instance.SetIcon();
    }
    public void StartAdsSpeedUP()
    {
        AdsTutorialSpeed.SetActive(true);
    }
    public void AdsSpeedUP_2()
    {
        AdsTutorialSpeed.SetActive(false);
        AdsTutorialSpeed_2.SetActive(true);
    }
    public void EndAdsSpeedUP()
    {
        AdsTutorialSpeed_2.SetActive(false);

        GameManager.Instance.BEndAdsSpeed = true;
        UIManager.Instance.SetIcon();
    }


    public void StartAdsGold()
    {
        AdsTutorialGold.SetActive(true);
    }
    public void AdsGold_2()
    {
        AdsTutorialGold.SetActive(false);
        AdsTutorialGold_2.SetActive(true);
    }
    public void EndAdsGold()
    {
        AdsTutorialGold_2.SetActive(false);

        GameManager.Instance.bEndAdsGold = true;
        UIManager.Instance.SetIcon();
    }
    public void StartTouchTutorial()
    {
        TouchTutorial.SetActive(true);
    }
    int touchIndex = 0;
    GameObject Gem;
    GameObject StartCoin;
    public void TouchToutorial()
    {

        Gem = TouchTutorial.transform.Find("Gem").gameObject;
        StartCoin = TouchTutorial.transform.Find("StarCoin").gameObject;
        touchIndex++;
        if(touchIndex >=2)
        {

            StartCoroutine(endtouch());
        }
    }
    IEnumerator endtouch()
    {
        yield return new WaitForSeconds(0.2f);
        if(StartCoin.activeSelf == false && Gem.activeSelf ==false)
        {
            TouchTutorial.SetActive(false);
        }
        else
        {
            StartCoroutine(endtouch());
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
