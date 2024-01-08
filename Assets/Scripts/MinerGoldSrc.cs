using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinerGoldSrc : MonoBehaviour
{
    // Start is called before the first frame update
    private float defaultTime = 1f;
    private float initSpeed = 1f;
    public bool bStartMine = false;
    Material MyEffectMat;
    void Start()
    {
        MyEffectMat = this.GetComponent<SpriteRenderer>().material;
        StartCoroutine(StartRoutine());
        //StartCoroutine(GetMoneyRoutine(defaultTime));
    }
    public void SetSpeedUP()
    {
        //initSpeed = defaultTime;           
    }

    public void EndSpeedUP()
    {
        defaultTime = initSpeed;
    }
    IEnumerator StartRoutine()
    {
        float rand = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand);
        StartCoroutine(GetMoneyRoutine(defaultTime));
    }
    public void SetStartMine(bool flag)
    {
        bStartMine = flag;
    }
    public void SetDeleteMat()
    {
        MyEffectMat = GameManager.Instance.DefaultMat;
        this.GetComponent<SpriteRenderer>().material = MyEffectMat;
    }
    public void SetEffect()
    {
        if (GameManager.Instance.bSpeedUp == true)
        {
            MyEffectMat = GameManager.Instance.EffectMat;
            this.GetComponent<SpriteRenderer>().material = MyEffectMat;
            this.GetComponent<Animator>().speed = 2f;
        }
        else
        {
            MyEffectMat = GameManager.Instance.DefaultMat;
            this.GetComponent<SpriteRenderer>().material = MyEffectMat;
            this.GetComponent<Animator>().speed = 1;
        }
    }
    IEnumerator GetMoneyRoutine(float _time)
    {
        float MineTime = 1;
        if (GameManager.Instance.bSpeedUp ==true)
        {
            MineTime = defaultTime / 3;            
        }
        else
        {
            MineTime = defaultTime;            
        }
        //bug.Log(this.GetComponent<Animator>().speed);
        yield return new WaitForSeconds(MineTime);        
        if(bStartMine == true)
        {
            if (GameManager.Instance.TextEffectCount >= GameManager.Instance.TextEffectPool.Count - 1)
            {
                GameManager.Instance.TextEffectCount = 0;
            }
            GameManager.Instance.TextEffectPool[GameManager.Instance.TextEffectCount].transform.SetParent(this.transform.parent);
            Vector3 MyPos = this.transform.localPosition;
            MyPos.y = MyPos.y + 105;
            int iOut;
            if (int.TryParse(this.name, out iOut) == true)
            {                
                MyPos.x = MyPos.x - GameManager.Instance.minerPosVector[iOut - 1].x;
                MyPos.y = MyPos.y - GameManager.Instance.minerPosVector[iOut - 1].y;
                GameManager.Instance.TextEffectPool[GameManager.Instance.TextEffectCount].transform.localPosition = MyPos;
            }
            else
            {
                GameManager.Instance.TextEffectPool[GameManager.Instance.TextEffectCount].transform.localPosition = MyPos;
            }            
            GameManager.Instance.TextEffectPool[GameManager.Instance.TextEffectCount].SetActive(true);
            int name;
            if(int.TryParse(this.name,out name))
            {
                GameManager.Instance.TextEffectPool[GameManager.Instance.TextEffectCount].GetComponent<Text>().text = "+ " + GameManager.Instance.ChangeFormat(GameManager.Instance.GainPerSecMiner[name - 1] * GameManager.Instance.AdsGoldPower);
                UIManager.Instance.SetTotalMoney(GameManager.Instance.GainPerSecMiner[name - 1] * GameManager.Instance.AdsGoldPower);
            }            
            GameManager.Instance.TextEffectCount++;
          
        }
     
        StartCoroutine(GetMoneyRoutine(MineTime));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
