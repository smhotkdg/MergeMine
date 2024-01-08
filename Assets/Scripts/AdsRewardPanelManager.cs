using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdsRewardPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AdsMinerObj;
    public GameObject TimeRewardObj;
    public GameObject SpeedUPObj;
    public List<GameObject> MinerList;
    public Text MinerNumber;
    public Text TimeGoldText;
    void Start()
    {
        
    }

    public void SetInitData(string name)
    {
        AdsMinerObj.SetActive(false);
        TimeRewardObj.SetActive(false);
        SpeedUPObj.SetActive(false);
        switch (name)
        {
            case "Miner":
                for (int i = 0; i < MinerList.Count; i++)
                {
                    MinerList[i].SetActive(false);
                }
                AdsMinerObj.SetActive(true);

                if (GameManager.Instance.MaxMergetNumber - 3 > 0)
                {
                    MinerNumber.text = (GameManager.Instance.MaxMergetNumber - 3).ToString();
                    MinerList[GameManager.Instance.MaxMergetNumber - 3 - 1].SetActive(true);
                }
                else
                {
                    MinerNumber.text = GameManager.Instance.MaxMergetNumber.ToString();
                    MinerList[GameManager.Instance.MaxMergetNumber - 1].SetActive(true);
                }

                break;
            case "Time":
                TimeRewardObj.SetActive(true);
                TimeGoldText.text = GameManager.Instance.ChangeFormat(GameManager.Instance.timerewardDoublemoney);
                break;
            case "Gold":
                break;
            case "AdsGold":
                TimeRewardObj.SetActive(true);
                TimeGoldText.text = GameManager.Instance.ChangeFormat(GameManager.Instance.BoxRewardMoney*3);
                break;
            case "SpeedUp":
                SpeedUPObj.SetActive(true);
                break;
            case "AdsGold_Drill":
                TimeRewardObj.SetActive(true);
                TimeGoldText.text = GameManager.Instance.ChangeFormat(GameManager.Instance.DrillGold);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
