using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewMinerBuyPanelManager : MonoBehaviour
{
   
    public List<GameObject> MinerList;

    public Text MinerNumberText;
    void Start()
    {
        this.GetComponent<Animator>().SetBool("isEnd", false);
    }

    // Update is called once per frame
    public void SetIInitMiner()
    {
        for(int i=0; i< MinerList.Count; i++)
        {
            if (MinerList[i].activeSelf == true)
                MinerList[i].SetActive(false);
        }
        MinerList[GameManager.Instance.GetminerLevel()].SetActive(true);
        MinerNumberText.text = (GameManager.Instance.GetminerLevel() + 1).ToString();
    } 

    void Update()
    {
       
    }
    private void OnEnable()
    {        
        this.GetComponent<Animator>().SetBool("isEnd", false);
    }
    public void ClosePanel()
    {
        this.GetComponent<Animator>().SetBool("isEnd", true);
    }
    public void EndAnim()
    {
        this.GetComponent<Animator>().SetBool("isEnd", false);
        UIManager.Instance.SetNewMinerBuyPanel(false);
    }
}
