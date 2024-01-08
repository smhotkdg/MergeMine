using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerManager : MonoBehaviour
{
    // Use this for initialization
    private static TimerManager _instance = null;

    public static TimerManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton TimerManager == null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {            
            _instance = this;
            OldTime = System.DateTime.Now;
            LoadData();
        } 
    }
    void SaveData()
    {
        OldTime = System.DateTime.Now;
        TimerManager.Instance.AddTimer("AppQuitTime");
        
    }
    private void OnApplicationPause(bool pause)
    {
       if(pause == true)
        {            
            SaveData();
        }
        else
        {            
            OldTime = System.DateTime.Now;
            LoadData();

            //여기확인
            LoadTime();
            CheckRewarad();
        }
    }
    private void OnApplicationQuit()
    {
        SaveData();
    }
    public void CheckRewarad()
    {
        float Time = (float)GetTime("AppQuitTime");
        if (Time > 120)
        {
            //GUIManager.Instance.ShowOfflineReward(true, Time);
            
            if(GameManager.Instance.RewardMinerPower >0)
            {
#if UNITY_EDITOR
                UIManager.Instance.SetTimeRewardPanel(true, Time);
                GameManager.Instance.SetTimeRewardBox();
#endif
#if !UNITY_EDITOR
                StartCoroutine(CheckTimeReward(Time));          
#endif
            }
        }
    }
    IEnumerator CheckTimeReward(float Time)
    {
        yield return new WaitForSeconds(1f);
        if(AdManager.Instance.CheckReady() == true)
        {
            UIManager.Instance.SetTimeRewardPanel(true, Time);
            GameManager.Instance.SetTimeRewardBox();
        }
        else
        {
            StartCoroutine(CheckTimeReward(Time));
        }
    }
    float loadTime;
    void LoadData()
    {
        loadTime = (float)GetTime("AppQuitTime");
        
    }
    
    public string FloatToTime(float toConvert, string format)
    {
        switch (format)
        {
            case "00.0":
                return string.Format("{0:00}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "#0.0":
                return string.Format("{0:#0}:{1:0}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "00.00":
                return string.Format("{0:00}:{1:00}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "00.000":
                return string.Format("{0:00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#00.000":
                return string.Format("{0:#00}:{1:000}",
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#0:00":
                return string.Format("{0:#0}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
                break;
            case "#00:00":
                return string.Format("{0:#00}:{1:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60);//seconds
                break;
            case "0:00.0":
                return string.Format("{0:0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "#0:00.0":
                return string.Format("{0:#0}:{1:00}.{2:0}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 10) % 10));//miliseconds
                break;
            case "0:00.00":
                return string.Format("{0:0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "#0:00.00":
                return string.Format("{0:#0}:{1:00}.{2:00}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 100) % 100));//miliseconds
                break;
            case "0:00.000":
                return string.Format("{0:0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
            case "#0:00.000":
                return string.Format("{0:#0}:{1:00}.{2:000}",
                    Mathf.Floor(toConvert / 60),//minutes
                    Mathf.Floor(toConvert) % 60,//seconds
                    Mathf.Floor((toConvert * 1000) % 1000));//miliseconds
                break;
        }
        return "error";
    }
    public void LoadTime()
    {        
        //Debug.Log("Time : " + Time);
        GameManager.Instance.SpinTime -= loadTime;
        GameManager.Instance.GoldRushTime -= loadTime;
        GameManager.Instance.FreeGoldBarTime -= loadTime;

        GameManager.Instance.AdsGoldTime -= loadTime;
        GameManager.Instance.AdsSpeedUpTime -= loadTime;
    }

    private void Update()
    {
        if (GameManager.Instance.SpinTime <= 0)
        {           
            GameManager.Instance.SpinTime = 0;
            GameManager.Instance.AdsGoldPower = 1;
            GameManager.Instance.isSpin = true;
            UIManager.Instance.SetSpinAnim(true);
        }
        else
        {
            GameManager.Instance.SpinTime -= Time.deltaTime;
            UIManager.Instance.SetSpinAnim(false);
        }

        if (GameManager.Instance.GoldRushTime <= 0)
        {
            GameManager.Instance.GoldRushTime = 0;
            UIManager.Instance.SetTrainExclamationMark(true);
        }
        else
        {
            GameManager.Instance.GoldRushTime -= Time.deltaTime;
            UIManager.Instance.SetTrainExclamationMark(false);
        }

        if (GameManager.Instance.FreeGoldBarTime <= 0)
        {
            GameManager.Instance.FreeGoldBarTime = 0;            
        }
        else
        {
            GameManager.Instance.FreeGoldBarTime -= Time.deltaTime;            
        }


        if (GameManager.Instance.AdsGoldTime <= 0)
        {
            GameManager.Instance.AdsGoldTime = 0;
        }
        else
        {
            GameManager.Instance.AdsGoldTime -= Time.deltaTime;
        }


        if (GameManager.Instance.AdsSpeedUpTime <= 0)
        {
            GameManager.Instance.AdsSpeedUpTime = 0;
        }
        else
        {
            GameManager.Instance.AdsSpeedUpTime -= Time.deltaTime;
        }
    }
    public void AddTimer(string key)
    {
        string timerKeyStr = key;
        string now = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        PlayerPrefs.SetString(timerKeyStr, now);
        PlayerPrefs.Save();
    }
 
    public double GetTime(string key)
    {
        System.TimeSpan SpanTime;
        SpanTime = CheckTimer(key);
        if (SpanTime != System.TimeSpan.Zero)
        {
            return SpanTime.TotalSeconds;            
        }
        else
        {
            return 0;
        }
    }

    System.DateTime OldTime;


    System.TimeSpan CheckTimer(string name)
    {
        System.TimeSpan AAA = System.TimeSpan.Zero;
        string key = name;
        string startTimeStr = PlayerPrefs.GetString(key);
        if (startTimeStr != "")
        {
            System.DateTime start = System.DateTime.Parse(startTimeStr);
            System.DateTime LoginTime = start;
            AAA = OldTime - LoginTime;
            return AAA;
        }
        else
        {
            return AAA;
        }
    }
}
