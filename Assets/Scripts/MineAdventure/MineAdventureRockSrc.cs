using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MineAdventureRockSrc : MonoBehaviour
{
    public delegate void OnClickEvent(bool isFree);
    public event OnClickEvent OnClick;


    public delegate void OnGetEvent(string name);
    public event OnGetEvent OnGet;

    Ray rayUp;
    Ray rayLeft;
    Ray rayRight;
    Ray rayDown;
    float distance = 0.2f;
    public float hp;
    public float defaultHP;
    void Start()
    {
        distance = (GameManager.Instance.DrillAdventureTotalDigRangeCount - 1) * 0.2f;
        rayUp = new Ray();
        rayUp.origin = transform.position;
        Vector3 rayvecUp = new Vector3();
        rayvecUp = transform.position;
        rayvecUp.y += 0.5f;
        rayUp.origin = rayvecUp;
        rayUp.direction = transform.up;



        rayLeft = new Ray();
        rayLeft.origin = transform.position;
        Vector3 rayvecLeft = new Vector3();
        rayvecLeft = transform.position;
        rayvecLeft.x += 0.5f;
        rayLeft.origin = rayvecLeft;
        rayLeft.direction = -transform.right;


        rayRight = new Ray();
        rayRight.origin = transform.position;
        Vector3 rayvecrayRight = new Vector3();
        rayvecrayRight = transform.position;
        rayvecrayRight.x -= 0.5f;
        rayRight.origin = rayvecrayRight;
        rayRight.direction = transform.right;


        rayDown = new Ray();
        rayDown.origin = transform.position;
        Vector3 rayvecrayDown = new Vector3();
        rayvecrayDown = transform.position;
        rayvecrayDown.y -= 0.5f;
        rayDown.origin = rayvecrayDown;
        rayDown.direction = -transform.up;


        Debug.DrawRay(rayUp.origin, rayUp.direction * distance, Color.yellow, 200f);

        Debug.DrawRay(rayDown.origin, rayDown.direction * distance, Color.yellow, 200f);

        Debug.DrawRay(rayLeft.origin, rayLeft.direction * distance, Color.yellow, 200f);

        Debug.DrawRay(rayRight.origin, rayRight.direction * distance, Color.yellow, 200f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
      
    }

    public void SetHp(float _hp)
    {
        hp = _hp;
        defaultHP = _hp;
        transform.Find("HP/Text").GetComponent<Text>().text = hp.ToString();
    }
    public void RockClickFree()
    {
        hp -= GameManager.Instance.DigPower;
        if (hp <= 0)
        {
            if(this.GetComponent<Image>().enabled ==true)
            {
                this.GetComponent<Image>().enabled = false;
                if (this.transform.childCount > 0)
                {
                    this.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    OnGet(this.transform.GetChild(0).name);
                }
            }         
        }
        CheckHp();
        SetText();
        OnClick(true);
    }
    public void CheckHp()
    {
        SoundsManager.Instance.SetRockSound();
        float percent = hp / defaultHP;

        if(percent < 0.1f)
        {
            string RockName = "Rock/Rock_10";
            GameObject prefabObj = Resources.Load(RockName) as GameObject;
            GameObject RockObj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
            GetComponent<Image>().sprite = RockObj.GetComponent<Image>().sprite;
            Destroy(RockObj);
        }
        else if(percent < 0.5f)
        {
            string RockName = "Rock/Rock_30";
            GameObject prefabObj = Resources.Load(RockName) as GameObject;
            GameObject RockObj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
            GetComponent<Image>().sprite = RockObj.GetComponent<Image>().sprite;
            Destroy(RockObj);
        }
        else if (percent < 0.8f)
        {
            string RockName = "Rock/Rock_50";
            GameObject prefabObj = Resources.Load(RockName) as GameObject;
            GameObject RockObj = MonoBehaviour.Instantiate(prefabObj) as GameObject;
            GetComponent<Image>().sprite = RockObj.GetComponent<Image>().sprite;
            Destroy(RockObj);
        }
    }
    public void SetText()
    {
        if (hp < 0)
        {
            hp = 0;            
        }
        if(hp ==0)
            transform.Find("HP/Text").gameObject.SetActive(false);

        transform.Find("HP/Text").GetComponent<Text>().text = hp.ToString();
        transform.Find("HP").GetComponent<Image>().fillAmount = (float)(hp / defaultHP);
    }
    public void RockClick()
    {
        if (GameManager.Instance.DrillAdventureTotalDigCount_Now > 0)
        {
            if (distance > 0)
            {
                RaycastHit2D[] hitsList;
                hitsList = Physics2D.RaycastAll(rayUp.origin, rayUp.direction * distance);
                for (int i = 0; i < hitsList.Length; i++)
                {
                    if (hitsList[i].collider.tag == "Rock" && hitsList[i].distance < 0.6f * (GameManager.Instance.DrillAdventureTotalDigRangeCount - 1))
                    {
                        hitsList[i].transform.GetComponent<MineAdventureRockSrc>().RockClickFree();
                    }
                }

                RaycastHit2D[] hitsDownList;
                hitsDownList = Physics2D.RaycastAll(rayDown.origin, rayDown.direction * distance);
                for (int i = 0; i < hitsDownList.Length; i++)
                {
                    if (hitsDownList[i].collider.tag == "Rock" && hitsDownList[i].distance < 0.6f * (GameManager.Instance.DrillAdventureTotalDigRangeCount - 1))
                    {
                        hitsDownList[i].transform.GetComponent<MineAdventureRockSrc>().RockClickFree();
                    }
                }

                RaycastHit2D[] hitsLeftList;
                hitsLeftList = Physics2D.RaycastAll(rayLeft.origin, rayLeft.direction * distance);
                for (int i = 0; i < hitsLeftList.Length; i++)
                {
                    if (hitsLeftList[i].collider.tag == "Rock" && hitsLeftList[i].distance < 0.6f * (GameManager.Instance.DrillAdventureTotalDigRangeCount - 1))
                    {
                        hitsLeftList[i].transform.GetComponent<MineAdventureRockSrc>().RockClickFree();
                    }
                }

                RaycastHit2D[] hitsRightList;
                hitsRightList = Physics2D.RaycastAll(rayRight.origin, rayRight.direction * distance);
                for (int i = 0; i < hitsRightList.Length; i++)
                {
                    if (hitsRightList[i].collider.tag == "Rock" && hitsRightList[i].distance < 0.6f * (GameManager.Instance.DrillAdventureTotalDigRangeCount - 1))
                    {
                        hitsRightList[i].transform.GetComponent<MineAdventureRockSrc>().RockClickFree();
                    }
                }
            }

            hp -= GameManager.Instance.DigPower;
            if (hp <= 0)
            {
                if(this.GetComponent<Image>().enabled == true)
                {
                    this.GetComponent<Image>().enabled = false;
                    this.GetComponent<Button>().interactable = false;
                    if (this.transform.childCount > 0)
                    {
                        this.transform.GetChild(0).GetComponent<Image>().enabled = true;
                        OnGet(this.transform.GetChild(0).name);
                    }
                }            
            }
            CheckHp();
            OnClick(false);
            SetText();
        }
    }
   
}
