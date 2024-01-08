using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    // Start is called before the first frame update    
    public AudioSource MiningAdventureBGM;
    public AudioSource Rock;

    public AudioSource Enter;
    public AudioSource Open;

    public AudioSource Drillking;
    public AudioSource Perfect;
    public AudioSource Good;
    public AudioSource Oops;
    public AudioSource Nut;


    public AudioSource SpinStart;
    public AudioSource SpinEnd;
    public AudioSource TrainStart;
    public AudioSource Train;
    public List<AudioSource> Coins;
    public AudioSource BGM;
    public AudioSource BGM_2;
    public AudioSource Button;
    public AudioSource BuyMiner;
    public AudioSource NewMiner;
    public AudioSource Box;
    public AudioSource Merge;
    public AudioSource Reward;


    private static SoundsManager _instance = null;

    public static SoundsManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton SoundsManager == null");
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
            //
            _instance = this;            
        }
    }
    public void OpenSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Open.Play();
        }
    }
    public void EnterSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Enter.Play();
        }
    }

    public void PerfectSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Perfect.Play();
        }
    }
    public void NutSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Nut.Play();
        }
    }
    public void OopsSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Oops.Play();
        }
    }
    public void GoodSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Good.Play();
        }
    }
    public void SpinStartSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            SpinStart.Play();
        }
    }
    public void SpinStopSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            SpinEnd.Play();
        }
    }
    public void TrainStartSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            TrainStart.Play();
        }
    }
    public void TrainSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Train.Play();
        }
    }
    public void StopTrain()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Train.Stop();
        }
    }
    public void BuyMinerSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            BuyMiner.Play();
        }
    }
    public void NewMinerSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (NewMiner.isPlaying == false)
            NewMiner.Play();
        }
    }
    public void SetMiningAdventureBGM(bool flag)
    {
        if (GameManager.Instance.BGM == true)
        {
            if (flag == true)
            {
                BGM.Stop();
                MiningAdventureBGM.Play();
            }
            else
            {
                BGM.Play();
                MiningAdventureBGM.Stop();
            }
        }
        else
        {
            return;
        }
    }
    public void SetRockSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (BuyMiner.isPlaying == false)
            Rock.Play();
        }
    }

    public void SetDrillKingSound(bool flag)
    {
        if (GameManager.Instance.BGM == true)
        {
            if(flag ==true)
            {
                BGM.Stop();
                Drillking.Play();
            }
            else
            {
                BGM.Play();
                Drillking.Stop();
            }
        }
        else
        {
            return;
        }
    }
    public void ButtonClick()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (Button.isPlaying == false)
            Button.Play();
        }
    }
    public void CoinsSound(int index)
    {
        if (GameManager.Instance.Fx == true)
        {
            StartCoroutine(CoinsSoundStart(index));
        }
    }
    IEnumerator CoinsSoundStart(int index)
    {
        for(int i=0; i< index; i++)
        {
            //if (Coins[i].isPlaying == false)
            Coins[i].Play();
            yield return new WaitForSeconds(0.02f);
        }
        
    }
    public void BoxSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            if (Box.isPlaying == false)
                Box.Play();
        }
    }
    public void RewardSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (Reward.isPlaying == false)
            Reward.Play();
        }
    }
    public void MergeSound()
    {
        if (GameManager.Instance.Fx == true)
        {
            //if (Merge.isPlaying == false)
            Merge.Play();
        }
    }
    public void MuteBGM()
    {
        if(GameManager.Instance.BGM == true)
        {
            BGM.Play();
        }
        else
        {
            BGM.Stop();
        }
    }
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
