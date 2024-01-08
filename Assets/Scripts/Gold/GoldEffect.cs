using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidibody;
    GameObject Gold;

    Rigidbody2D Gemrigidibody;
    GameObject Gem;

    Rigidbody2D starrigidibody;
    GameObject star;

    float defaultSpeed = 1f;
    void Start()
    {
        StartCoroutine(RockEventRoutine(defaultSpeed));
        //StartCoroutine(GetNut(1));
        StartCoroutine(GetGem(1));
        StartCoroutine(GetStar(1));
    }
    IEnumerator GetStar(float sec)
    {
        yield return new WaitForSeconds(120);
        int rand = Random.Range(0, 300);
        if (rand < 5)
        {
            ClickStarCoin();
        }
        
        StartCoroutine(GetStar(30));
    }

    IEnumerator GetGem(float sec)
    {
        yield return new WaitForSeconds(30);
        int rand = Random.Range(0, 100);
        if (rand < 15)
        {
            ClickGem();
        }
        
        StartCoroutine(GetGem(30));
    }

    IEnumerator GetNut(float sec)
    {
        int rand = Random.Range(0, 100);
        if (rand <10)
        {
            ClickNut();
        }
        yield return new WaitForSeconds(sec);
        StartCoroutine(GetNut(sec));
    }
    IEnumerator RockEventRoutine(float speed)
    {
        int rand = Random.Range(0, 100);
        if(rand < 10)
        {
            ClickRock();
        }
        //if (GameManager.Instance.bSpeedUp == true)
        if (GameManager.Instance.AdsGoldPower > 1)
        {
            ClickRock();
            defaultSpeed = 0.3f;
        }
        else
        {
            defaultSpeed = 1f;
        }       
        yield return new WaitForSeconds(defaultSpeed);
        StartCoroutine(RockEventRoutine(defaultSpeed));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickGem()
    {
        this.GetComponent<Animator>().SetBool("isRock", true);
        Gem = GameManager.Instance.GetGem(this.gameObject);
        if (Gem == null)
            return;
        Gemrigidibody = Gem.GetComponent<Rigidbody2D>();
        Gem.SetActive(true);
        int rand = Random.Range(0, 2);
        int randRightPower = Random.Range(60, 90);
        int randUpPower = Random.Range(90, 250);
        if (rand == 0)
        {
            Gemrigidibody.AddForce(transform.right * randRightPower);
        }
        else
        {
            Gemrigidibody.AddForce(-transform.right * randRightPower);
        }

        Gemrigidibody.AddForce(transform.up * randUpPower);
    }
    public void ClickStarCoin()
    {
        this.GetComponent<Animator>().SetBool("isRock", true);
        star = GameManager.Instance.GetStarCoin(this.gameObject);
        if (star == null)
            return;
        starrigidibody = star.GetComponent<Rigidbody2D>();
        star.SetActive(true);
        int rand = Random.Range(0, 2);
        int randRightPower = Random.Range(60, 90);
        int randUpPower = Random.Range(90, 250);
        if (rand == 0)
        {
            starrigidibody.AddForce(transform.right * randRightPower);
        }
        else
        {
            starrigidibody.AddForce(-transform.right * randRightPower);
        }

        starrigidibody.AddForce(transform.up * randUpPower);
    }
    public void ClickNut()
    {
        this.GetComponent<Animator>().SetBool("isRock", true);
        Gold = GameManager.Instance.GetNut(this.gameObject);
        if (Gold == null)
            return;
        rigidibody = Gold.GetComponent<Rigidbody2D>();
        Gold.SetActive(true);
        int rand = Random.Range(0, 2);
        int randRightPower = Random.Range(60, 90);
        int randUpPower = Random.Range(90, 250);
        if (rand == 0)
        {
            rigidibody.AddForce(transform.right * randRightPower);
        }
        else
        {
            rigidibody.AddForce(-transform.right * randRightPower);
        }

        rigidibody.AddForce(transform.up * randUpPower);
    }

    public void ClickRock()
    {
        this.GetComponent<Animator>().SetBool("isRock", true);
        Gold = GameManager.Instance.SetGold(this.gameObject);
        rigidibody = Gold.GetComponent<Rigidbody2D>();        
        Gold.SetActive(true);
        int rand = Random.Range(0, 2);
        int randRightPower = Random.Range(60, 90);
        int randUpPower = Random.Range(90, 250);
        if (rand  ==0)
        {
            rigidibody.AddForce(transform.right * randRightPower);
        }
        else
        {
            rigidibody.AddForce(-transform.right * randRightPower);
        }        
        
        rigidibody.AddForce(transform.up * randUpPower);
    }
    public void EndAnim()
    {
        this.GetComponent<Animator>().SetBool("isRock", false);
    }
}
