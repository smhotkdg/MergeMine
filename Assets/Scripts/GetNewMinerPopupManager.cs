using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetNewMinerPopupManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EffectParent;
    GameObject MinerLeft;
    GameObject MinerRight;
    GameObject MinerCenter;
    public GameObject Effect;
    public Text MoneyText;
    public int index = 0;
    void Start()
    {

    }
    private void OnEnable()
    {
        
    }
    
    public void SetMinerData(int minerindex)
    {
        index = minerindex;
        MergeAnim();
        if(GameManager.Instance.GainPerSecMiner.Count  == index)
        {
            MoneyText.text = "+ " + GameManager.Instance.ChangeFormat(GameManager.Instance.GainPerSecMiner[index-1] * 6000);
            //GameManager.Instance.AddTotalMoney((GameManager.Instance.GainPerSecMiner[index-1] * 6000));
            
        }
        else
        {
            MoneyText.text = "+ " + GameManager.Instance.ChangeFormat(GameManager.Instance.GainPerSecMiner[index] * 60);
            //GameManager.Instance.AddTotalMoney((GameManager.Instance.GainPerSecMiner[index] * 60));
        }

        GameManager.Instance.totalGem++;
        GameManager.Instance.totalStarCoin++;
        UIManager.Instance.SetTotalGemText();
        UIManager.Instance.SetTotalStarCoinText();

        UIManager.Instance.SetMoney();        
    }
    private void OnDisable()
    {
        if (MinerLeft != null)
            Destroy(MinerLeft);
        if(MinerRight !=null)
            Destroy(MinerRight);
        if (MinerCenter != null)
            Destroy(MinerCenter);
        Effect.SetActive(false);
    }

    void MergeAnim()
    {
        //x +-0.4
        string prepabId = "Prefabs/Miner" + (index-1);
        string prepabId_center = "Prefabs/Miner" + index;
        GameObject MinerObjPrefab = Resources.Load(prepabId) as GameObject;
        GameObject MinerObjPrefab_center = Resources.Load(prepabId_center) as GameObject;
        if (MinerObjPrefab == null)
            return;
        MinerLeft = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        MinerRight = MonoBehaviour.Instantiate(MinerObjPrefab) as GameObject;
        MinerCenter = MonoBehaviour.Instantiate(MinerObjPrefab_center) as GameObject;

        MinerCenter.transform.SetParent(EffectParent.transform);
        MinerCenter.GetComponent<SpriteRenderer>().sortingLayerName = "PopupEffect";
        //MinerCenter.transform.localPosition = new Vector3(0, 0, 0);
        MinerCenter.transform.localPosition  = GameManager.Instance.minerPosVector[index - 1];
        MinerLeft.transform.SetParent(EffectParent.transform);
        MinerRight.transform.SetParent(EffectParent.transform);
        MinerLeft.transform.localScale = MinerCenter.transform.localScale;
        MinerRight.transform.localScale = MinerCenter.transform.localScale;
        MinerLeft.GetComponent<SpriteRenderer>().sortingLayerName = "PopupEffect";
        MinerRight.GetComponent<SpriteRenderer>().sortingLayerName = "PopupEffect";

        //Vector3 LeftPos = new Vector3(0,0,0);
        Vector3 LeftPos = GameManager.Instance.minerPosVector[index - 2];
        LeftPos.x = LeftPos.x - 75;
        MinerLeft.transform.localPosition = LeftPos;

        //Vector3 RightPos = new Vector3(0, 0, 0);
        Vector3 RightPos = GameManager.Instance.minerPosVector[index - 2];
        RightPos.x = RightPos.x + 75;
        MinerRight.transform.localPosition = RightPos;
        MinerCenter.SetActive(false);
        StartCoroutine(MoveAnim(MinerLeft, MinerRight, MinerCenter));
    }
    IEnumerator MoveAnim(GameObject M1, GameObject M2, GameObject MinerObj)
    {
        yield return new WaitForSeconds(0.1f);
        SoundsManager.Instance.NewMinerSound();
        for (int i = 0; i <10; i++)
        {
            Vector3 MoveVec = M1.transform.localPosition;
            MoveVec.x -= 5;
            M1.transform.localPosition = MoveVec;

            MoveVec = M2.transform.localPosition;
            MoveVec.x += 5;
            M2.transform.localPosition = MoveVec;
            yield return new WaitForSeconds(0.01f);
        }
        float speed = 1.5f;
        for (int i = 0; i < 20; i++)
        {
            Vector3 MoveVec = M1.transform.localPosition;
            MoveVec.x += speed;
            M1.transform.localPosition = MoveVec;

            MoveVec = M2.transform.localPosition;
            MoveVec.x -= speed;
            M2.transform.localPosition = MoveVec;
            yield return new WaitForSeconds(0.01f);
            speed += 0.5f;
            if (i == 5)
            {
                Effect.SetActive(true);
            }
        }
        //CollectionParticleManager.Instance.StartCoinParticle(10);
        CollectionParticleManager.Instance.StartGemParticle(1);
        CollectionParticleManager.Instance.StartStarCoinParticle(1);
        SoundsManager.Instance.CoinsSound(5);
        Destroy(M1);
        Destroy(M2);
        MinerObj.SetActive(true);        

        for (int i = 0; i < 5; i++)
        {
            if (MinerObj != null)
            {
                Vector3 ScaleVec = MinerObj.transform.localScale;
                ScaleVec.x += 20f;
                ScaleVec.y += 20f;
                MinerObj.transform.localScale = ScaleVec;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                Debug.Log("Coroutine MinerObj Null");
            }
        }
        for (int i = 0; i < 5; i++)
        {
            if (MinerObj != null)
            {
                Vector3 ScaleVec = MinerObj.transform.localScale;
                ScaleVec.x -= 20f;
                ScaleVec.y -= 20f;
                MinerObj.transform.localScale = ScaleVec;
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                Debug.Log("Coroutine MinerObj Null");
            }
        }
        
    }
}
