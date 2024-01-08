using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MineAdventureGame : MonoBehaviour
{

    public delegate void OnEndGameEvent(bool isGold,bool isStarCoin, bool isGme, bool isNut);
    public event OnEndGameEvent OnEndGame;


    public delegate void OnEndGameCountEvent(int isGold, int isStarCoin, int isGme, int isNut);
    public event OnEndGameCountEvent OnEndGameCount;

    List<GameObject> RockList = new List<GameObject>();
    List<GameObject> DefaultRockList = new List<GameObject>();
    public List<Transform> Poslist;
    public List<Transform> doubleRockPos;

    public Image FillImage;
    public Text TotalDigCount;
    
    public int[,] rockPos = new int[7, 9];

    public Dictionary<int, int> weightsNormal = new Dictionary<int, int>();
    public void setWeightNoraml()
    {
        if (weightsNormal.Count == 4)
            return;   
        weightsNormal.Add(0, 10);
        weightsNormal.Add(1, 10);
        weightsNormal.Add(2, 20);
        weightsNormal.Add(3, 60);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    void SetData()
    {
        TotalDigCount.text = GameManager.Instance.DrillAdventureTotalDigCount_Now.ToString();
  
    }
    int[] rand;
    private void OnEnable()
    {        
        InitRock();
        GameManager.Instance.DrillAdventureTotalDigCount_Now =  GameManager.Instance.DrillAdventureTotalDigCount;
        FillImage.fillAmount =1;
        SetData();
        bStartEnd = false;
    }
    private void OnDisable()
    {
        for(int i=0; i< RockList.Count; i++)
        {
            Destroy(RockList[i]);
        }
        RockList.Clear();
        for (int i = 0; i < GetObejctList.Count; i++)
        {
            Destroy(GetObejctList[i]);
        }
        GetObejctList.Clear();

        for (int i = 0; i < DefaultRockList.Count; i++)
        {
            Destroy(DefaultRockList[i]);
        }
        DefaultRockList.Clear();

    }
    bool bStartEnd = false;
    void OnClick(bool isFree)
    {
        if(GameManager.Instance.DrillAdventureTotalDigCount_Now <=0)
        {
            return;
        }
        StartCoroutine(FillRoutine());
        if(isFree ==false)
            GameManager.Instance.DrillAdventureTotalDigCount_Now--;
        SetData();
        
        if(GameManager.Instance.DrillAdventureTotalDigCount_Now<=0)
        {
            if(bStartEnd ==false)
            {
                StartCoroutine(GameOverRoutine());
                bStartEnd = true;
            }                
        }

        int count = 0;

        for(int i =0; i< RockList.Count;i++)
        {
            if(RockList[i].GetComponent<Image>().isActiveAndEnabled==false)
            {
                count++;
            }             
        }
        if(count == RockList.Count)
        {
            if(bStartEnd == false)
            {
                StartCoroutine(GameOverRoutine());
                bStartEnd = true;
            }
            
        }
    }
    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.DrillAdventureGameOver();        
        OnEndGameCount(iGold, iStarCoin, iGem, iNut);
        OnEndGame(isGold, isStarCoin, isGem, isNut);
        isStarCoin = false;
        isGold = false;
        isGem = false;
        isNut = false;

        iStarCoin = 0;
        iGold = 0;
        iGem = 0;
        iNut = 0;
    }
    IEnumerator FillRoutine()
    {
        float now = (float)GameManager.Instance.DrillAdventureTotalDigCount_Now;
        float Temp = (float)1/10;
        for (int i=0; i < 10; i++)
        {            
            FillImage.fillAmount =  now/ (float)GameManager.Instance.DrillAdventureTotalDigCount;
            now -= Temp;
            yield return new WaitForSeconds(0.02f);
        }
    }

    List<GameObject> GetObejctList = new List<GameObject>();
    void InitRock()
    {
        //instantiate your dot in the bounds of that recttransform
        rand = GameManager.Instance.GetRandomInt(42, 0, 42);

        for (int i = 0; i < rand.Length; i++)
        {
            if(i < 15)
            {
                string RockName = "Rock/Rock_1";
                GameObject prefabObj = Resources.Load(RockName) as GameObject;
                GameObject RockObj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
                RockObj.transform.parent = Poslist[rand[i]];

                RockObj.name = "Rock_" + rand[i].ToString();
                RockObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                RockObj.transform.localPosition = new Vector3(0, 0, 0);
                RockObj.GetComponent<MineAdventureRockSrc>().SetHp(GameManager.Instance.DrillAdventureCount+1);
                RockObj.GetComponent<MineAdventureRockSrc>().OnClick += OnClick;
                RockObj.GetComponent<MineAdventureRockSrc>().OnGet += MineAdventureGame_OnGet;

                string RockObjectName = "Rock/Rock_Gold";
                GameObject prefabObj_Object;
                GameObject RockObj_Object;

                int randOwn = Random.Range(0, 100);
                if(randOwn >40)
                {
                    if (weightsNormal.Count == 0)
                        setWeightNoraml();
                    int normalMinerIndex = WeightedRandomizer.From(weightsNormal).TakeOne();
                    string name = "temp";
                    switch (normalMinerIndex)
                    {
                        case 3:
                            RockObjectName = "Rock/Rock_Gold";
                            name = "Gold";
                            break;
                        case 2:
                            RockObjectName = "Rock/Rock_Gem";
                            name = "Gem";
                            break;
                        case 0:
                            RockObjectName = "Rock/Rock_Nut";
                            name = "Nut";
                            break;
                        case 1:
                            RockObjectName = "Rock/Rock_StartCoin";
                            name = "StarCoin";
                            break;
                    }

                    prefabObj_Object = Resources.Load(RockObjectName) as GameObject;
                    RockObj_Object = MonoBehaviour.Instantiate(prefabObj_Object) as GameObject;
                    RockObj_Object.transform.parent = RockObj.transform;

                    RockObj_Object.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    RockObj_Object.transform.localPosition = new Vector3(0, 0, 0);
                    RockObj_Object.transform.name = name;
                    RockObj_Object.GetComponent<Image>().enabled = false;
                    RockObj_Object.transform.SetAsFirstSibling();
                    GetObejctList.Add(RockObj_Object);
                }
                
                RockList.Add(RockObj);
            }
            else
            {
                string RockName = "Rock/Rock_Default";
                GameObject prefabObj = Resources.Load(RockName) as GameObject;
                GameObject RockObj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
                RockObj.transform.parent = Poslist[rand[i]];

                RockObj.name = "Rock_Default_" + rand[i].ToString();
                RockObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                RockObj.transform.localPosition = new Vector3(0, 0, 0);
                DefaultRockList.Add(RockObj);
            }
       
        }
    }
    bool isStarCoin =false;
    bool isGold = false;
    bool isGem = false;
    bool isNut =false;

    int iStarCoin = 0;
    int iGold = 0;
    int iGem = 0;
    int iNut = 0;
    private void MineAdventureGame_OnGet(string name)
    {        
        switch (name)
        {
            case "Gold":
                iGold++;
                isGold = true;
                break;
            case "Gem":
                iGem++;
                isGem = true;
                GameManager.Instance.totalGem += 1;
                break;
            case "Nut":
                iNut++;
                isNut = true;
                GameManager.Instance.DrillPartList[GameManager.Instance.DrillAdventureCount] += 1;
                break;
            case "StarCoin":
                iStarCoin++;
                GameManager.Instance.totalStarCoin += 1;
                isStarCoin = true;
                break;
        }        
    }
}
