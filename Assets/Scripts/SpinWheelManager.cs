using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpinWheelManager : MonoBehaviour
{
    public GameObject SpinPan;

    public GameObject ArrowObj;
    public List<GameObject> PizzaList;
    public GameObject SpinButton;
    public GameObject CloseButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SetInitSpin();
    }
    void SetInitSpin()
    {
        if (GameManager.Instance.isSpin == true)
        {
            SpinButton.GetComponent<Button>().interactable = true;
            SpinButton.transform.Find("SpinText").gameObject.GetComponent<Text>().text = "SPIN";
        }
        else
        {
            SpinButton.GetComponent<Button>().interactable = false;
            SpinButton.transform.Find("SpinText").gameObject.GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.SpinTime, "#00:00");
        }
    }

    // Update is called once per frame        
    Quaternion vec;
    float z = 0;
    float RotationSpeed=0;
    void Update()
    {
        if (bStartSpin == true)
        {
            vec.eulerAngles = new Vector3(0, 0, z);
            SpinPan.transform.localRotation = vec;
            z += Time.deltaTime * RotationSpeed;

            RotationSpeed -= 5f * (Time.deltaTime * 100);
            if (RotationSpeed < 50)
            {
                RotationSpeed = 0;
                bStartSpin = false;
                EndSpin();
            }
            //z = 0;
            //bStartSpin = false;

        }
        if (GameManager.Instance.isSpin == false)
        {
            if (GameManager.Instance.SpinTime < 0)
            {
                GameManager.Instance.SpinTime = 0;
            }          
            else
            {
                if(bClick ==true)
                    SpinButton.transform.Find("SpinText").gameObject.GetComponent<Text>().text = TimerManager.Instance.FloatToTime(GameManager.Instance.SpinTime, "#00:00");
            }
        }
        else
        {
            if(bClick == true)
            {
                SpinButton.GetComponent<Button>().interactable = true;
                SpinButton.transform.Find("SpinText").gameObject.GetComponent<Text>().text = "SPIN";
            }
        }
    }
    public void EndSpin()
    {
        Quaternion myqu = SpinPan.transform.rotation;
        
        float angle = SpinPan.transform.localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;
        float pos = angle / 45;
        
        if(pos >0)
        {
            switch ((int)pos)
            {
                case 0:
                    GetReward(3,(int)pos);
                    break;
                case 1:
                    GetReward(2, (int)pos);
                    break;
                case 2:
                    GetReward(3, (int)pos);
                    break;
                case 3:
                    GetReward(2, (int)pos);
                    break;
                case 4:
                    GetReward(4, (int)pos);
                    break;
                case 5:
                    GetReward(2, (int)pos);
                    break;
                case 6:
                    GetReward(3, (int)pos);
                    break;
                case 7:
                    GetReward(2, (int)pos);
                    break;
            }
        }
        else
        {
            switch (-(int)pos)
            {
                case 0:
                    GetReward(2, 7);
                    break;
                case 1:
                    GetReward(3, 6);
                    break;
                case 2:
                    GetReward(2,5);
                    break;
                case 3:
                    GetReward(4, 4);
                    break;
                case 4:
                    GetReward(2, 3);
                    break;
                case 5:
                    GetReward(3,2);
                    break;
                case 6:
                    GetReward(2,1);
                    break;
                case 7:
                    GetReward(3, 0);
                    break;
            }
        }
        
    }
    public void GetReward(int index,int pizzacount)
    {
        SoundsManager.Instance.SpinStopSound();
        GameManager.Instance.AdsGoldPower = index;
        PizzaList[pizzacount].GetComponent<Animator>().SetBool("isGet", true);

        

        GameManager.Instance.isSpin = false;
        GameManager.Instance.SpinTime = GameManager.Instance.MaxSpinTime;
        SetInitSpin();
        bClick = true;
        CloseButton.SetActive(true);
    }
    bool bStartSpin = false;
    public void SpinStart()
    {
#if UNITY_EDITOR
        GameManager.Instance.adsIndex = 66182;
        GameManager.Instance.AdsRewardedAdCompleted();
#endif
#if !UNITY_EDITOR
    AdManager.Instance.ShowRewardedAds(66182);
#endif  
    }
    public void AdsTutorial()
    {        
        GameManager.Instance.adsIndex = 66182;
        GameManager.Instance.AdsRewardedAdCompleted();
    }
    bool bClick = true;
    private void OnDisable()
    {
        for (int i = 0; i < PizzaList.Count; i++)
        {
            PizzaList[i].GetComponent<Animator>().SetBool("isGet", false);
        }
    }
    public void SucessAds()
    {
        CloseButton.SetActive(false);
        for (int i = 0; i < PizzaList.Count; i++)
        {
            PizzaList[i].GetComponent<Animator>().SetBool("isGet", false);
        }
        z = 0;
        int rotationSpeed = Random.Range(750, 1400);
        RotationSpeed = rotationSpeed;
        vec = SpinPan.transform.localRotation;
        bStartSpin = true;
        bClick = false;
        SpinButton.GetComponent<Button>().interactable = false;
        GameManager.Instance.isSpin = false;
        SoundsManager.Instance.SpinStartSound();
    }
  
}
