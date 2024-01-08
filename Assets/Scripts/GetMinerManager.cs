using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetMinerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button BuyButton;
    public Text LevelText;
    public List<GameObject> MinerList;
    public Button NextButton;
    public Text Money;
    double cost;
    void Start()
    {
        
    }
    public void SetNextButton()
    {
        if(GameManager.Instance.MaxMergetNumber - 5> GameManager.Instance.minerNumber)
        {
            NextButton.interactable = true;
            UIManager.Instance.SetMinerColor(true);
        }
        else
        {
            NextButton.interactable = false;
            UIManager.Instance.SetMinerColor(false);
        }
    }
    public void SetData()
    {
        for (int i = 0; i < MinerList.Count; i++)
        {
            if (MinerList[i].activeSelf == true)
                MinerList[i].SetActive(false);
        }
        MinerList[GameManager.Instance.minerNumber - 1].SetActive(true);
        LevelText.text = GameManager.Instance.minerNumber.ToString();
        SetNextButton();
    }
    public void InitData()
    {
        cost = GameManager.Instance.DefaultMinerCost[GameManager.Instance.minerNumber - 1];
        for (int i=0; i< GameManager.Instance.TotalGetMinerCount[GameManager.Instance.minerNumber -1]; i++)
        {
            cost = cost * 1.11f;
        }
        Money.text = GameManager.Instance.ChangeFormat(cost);
        SetData();
    }
    public void UpdateCost()
    {       
        cost = GameManager.Instance.DefaultMinerCost[GameManager.Instance.minerNumber - 1];
        for (int i = 0; i < GameManager.Instance.TotalGetMinerCount[GameManager.Instance.minerNumber - 1]; i++)
        {          
            cost = cost * 1.11f;        
        }
        Money.text = GameManager.Instance.ChangeFormat(cost);
        SetData();
    }
    public double GetCost()
    {
        cost = GameManager.Instance.DefaultMinerCost[GameManager.Instance.minerNumber -1];
        for (int i = 0; i < GameManager.Instance.TotalGetMinerCount[GameManager.Instance.minerNumber -1]; i++)
        {
            cost = cost * 1.11f;
        }
        return cost;
    }
    // Update is called once per frame
    void Update()
    {
        if(cost <= GameManager.Instance.GetTotalMoney())
        {
            BuyButton.interactable = true;
        }
        else
        {
            BuyButton.interactable = false;
        }
    }
}
