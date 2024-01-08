using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform dst;
    public Transform p;
    public List<GameObject> CoinsListPool;

    public Transform dstGem;
    public Transform pGem;
    public List<GameObject> GemsListPool;


    public Transform dstStar;
    public Transform pStar;
    public List<GameObject> StarCoinListPool;

    private static CollectionParticleManager _instance = null;
    public static CollectionParticleManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton CollectionParticleManager == null");
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
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int poolindex = 0;
    public void StartCoinParticle(int count)
    {
        //y ~ 140 -180
        //x ~ 280 -280
        for(int i=0; i<count; i++)
        {
            if(CoinsListPool.Count-1 < i)
            {
                return;
            }
            poolindex++;
            if (poolindex > CoinsListPool.Count - 1)
                poolindex = 0;
            int randy = Random.Range(-180, 140);
            int randx = Random.Range(-280, 280);
            Vector3 initPos = new Vector3(randx, randy, 0);
            CoinsListPool[poolindex].GetComponent<CollectingAnimation>().Initialize(dst, p, initPos, new Vector3(1, 1, 1), CollectingAnimation.PLAY_SOUND_MODE.NONE, CollectingAnimation.EXPANSION_MODE.UPWARD);
            CoinsListPool[poolindex].GetComponent<CollectingAnimation>().StartAnimation();            
        }
       
    }

    int poolGemindex = 0;
    public void StartGemParticle(int count)
    {
        //y ~ 140 -180
        //x ~ 280 -280
        for (int i = 0; i < count; i++)
        {
            if (GemsListPool.Count - 1 < i)
            {
                return;
            }
            poolGemindex++;
            if (poolGemindex > GemsListPool.Count - 1)
                poolGemindex = 0;
            int randy = Random.Range(-180, 140);
            int randx = Random.Range(-280, 280);
            Vector3 initPos = new Vector3(randx, randy, 0);
            GemsListPool[poolGemindex].GetComponent<CollectingAnimation>().Initialize(dstGem, pGem, initPos, new Vector3(1, 1, 1), CollectingAnimation.PLAY_SOUND_MODE.NONE, CollectingAnimation.EXPANSION_MODE.UPWARD);
            GemsListPool[poolGemindex].GetComponent<CollectingAnimation>().StartAnimation();
        }

    }

    int poolStarIndex = 0;
    public void StartStarCoinParticle(int count)
    {
        //y ~ 140 -180
        //x ~ 280 -280
        for (int i = 0; i < count; i++)
        {
            if (StarCoinListPool.Count - 1 < i)
            {
                return;
            }
            poolStarIndex++;
            if (poolStarIndex > StarCoinListPool.Count - 1)
                poolStarIndex = 0;
            int randy = Random.Range(-180, 140);
            int randx = Random.Range(-280, 280);
            Vector3 initPos = new Vector3(randx, randy, 0);
            StarCoinListPool[poolStarIndex].GetComponent<CollectingAnimation>().Initialize(dstStar, pStar, initPos, new Vector3(1, 1, 1), CollectingAnimation.PLAY_SOUND_MODE.NONE, CollectingAnimation.EXPANSION_MODE.UPWARD);
            StarCoinListPool[poolStarIndex].GetComponent<CollectingAnimation>().StartAnimation();
        }

    }
}
