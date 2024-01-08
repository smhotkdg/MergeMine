using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinerCollectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> MinerList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        for(int i =0; i<MinerList.Count; i++)
        {
            int minerIndex = i + 1;
            MinerList[i].transform.Find("CollectionMinerLevelText").gameObject.GetComponent<Text>().text = minerIndex.ToString();
            if(minerIndex > GameManager.Instance.MaxMergetNumber)
            {
                MinerList[i].transform.Find("CollectionMiner").gameObject.SetActive(false);
                MinerList[i].transform.Find("Lock").gameObject.SetActive(true);
            }
            else
            {
                MinerList[i].transform.Find("CollectionMiner").gameObject.SetActive(true);
                MinerList[i].transform.Find("Lock").gameObject.SetActive(false);
            }
        }
    }
}
