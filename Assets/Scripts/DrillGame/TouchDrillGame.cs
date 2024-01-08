using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrillGame : MonoBehaviour
{
    // Start is called before the first frame update
    public nodeManager myNodeManager;
    public List<GameObject> NodeList;

    public List<GameObject> PerfectList;
    public List<GameObject> GoodList;
    public List<GameObject> OopsList;

    int nodecount = 0;
    int indexPerfect = 0;
    int indexGood = 0;
    int indexOops = 0;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        for(int i=0; i< NodeList.Count; i++)
        {
            NodeList[i].SetActive(false);
        }
        Shuffle(NodeList);
        bstart = false;
        StartCoroutine(GameRoutine(0.1f));
    }
    bool bstart = false;
    IEnumerator GameRoutine(float time)
    {
        if(bstart == false)
        {
            yield return new WaitForSeconds(3f);
            bstart = true;
        }
        float rand = Random.Range(0.5f, 1.2f);
        if (NodeList.Count - 1 < nodecount)
            nodecount = 0;
        NodeList[nodecount].transform.localPosition = new Vector2(0, 0);
        NodeList[nodecount].GetComponent<TouchObjectMoveAnim>().enabled = true;        
        NodeList[nodecount].SetActive(true);
        
        nodecount++;
        yield return new WaitForSeconds(rand);
        StartCoroutine(GameRoutine(rand));
    }
    // Update is called once per frame
    void Shuffle(List<GameObject> a)
    {
        // Loop array
        for (int i = a.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = UnityEngine.Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overwrite when we swap the values
            GameObject temp = a[i];

            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }

        // Print
        for (int i = 0; i < a.Count; i++)
        {
            Debug.Log(a[i]);
        }    }
    void Update()
    {
        
    }
  
    public void Click()
    {
        if(myNodeManager.percent >0.9)
        {
            SoundsManager.Instance.PerfectSound();
            if (PerfectList.Count - 1 < indexPerfect)
                indexPerfect = 0;
            PerfectList[indexPerfect].SetActive(true);
            indexPerfect++;
            myNodeManager.deleteObject();            
            GameManager.Instance.SetDrillSpeed(0.1f);
            
        }
        else if(myNodeManager.percent > 0.5f)
        {
            SoundsManager.Instance.GoodSound();
            if (GoodList.Count - 1 < indexGood)
                indexGood = 0;
            GoodList[indexGood].SetActive(true);
            indexGood++;
            myNodeManager.deleteObject();
            GameManager.Instance.SetDrillSpeed(0.05f);
        }
        else
        {
            SoundsManager.Instance.OopsSound();
            if (OopsList.Count - 1 < indexOops)
                indexOops = 0;
            OopsList[indexOops].SetActive(true);
            indexOops++;
            myNodeManager.deleteObjectoops();
            GameManager.Instance.SetDrillSpeed(-0.1f);
        }
        myNodeManager.percent = 0;
    }
}
