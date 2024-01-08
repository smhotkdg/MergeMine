using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GemMoveSrc : MonoBehaviour
{

    Rigidbody2D mybody;
    public Transform dst;
    Image Myimage;
    private void OnEnable()
    {
        if (Myimage != null)
            Myimage.enabled = true;
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
    private void Start()
    {
        mybody = transform.GetComponent<Rigidbody2D>();
        Myimage = transform.GetComponent<Image>();
    }
    public void ClickGold()
    {
        Vector3 initPos = transform.localPosition;
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(ScaleAnim());
        mybody.AddForce(Vector2.up * 300);
        mybody.AddForce(Vector2.left * 50);

        transform.GetComponent<CollectingAnimation>().Initialize(dst, transform.parent, initPos, new Vector3(1, 1, 1), CollectingAnimation.PLAY_SOUND_MODE.NONE, CollectingAnimation.EXPANSION_MODE.UPWARD);
        transform.GetComponent<CollectingAnimation>().StartAnimation();

        GameManager.Instance.TotalNutCount++;
    }
    IEnumerator ScaleAnim()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 scale = this.transform.localScale;
            scale.x -= 0.1f;
            scale.y -= 0.1f;
            this.transform.localScale = scale;
        }
        yield return new WaitForSeconds(0.01f);
    }
    private void Update()
    {
        if (transform.GetComponent<CollectingAnimation>().isEnd == true)
        {
            transform.GetComponent<CollectingAnimation>().isEnd = false;
            StartCoroutine(EndAnim());
        }
    }
    IEnumerator EndAnim()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 scale = this.transform.localScale;
            scale.x -= 0.2f;
            scale.y -= 0.2f;
            this.transform.localScale = scale;
        }
        yield return new WaitForSeconds(0.01f);

        if(this.name =="Gem")
        {
            GameManager.Instance.totalGem++;
            UIManager.Instance.SetTotalGemText();
        }
        if(this.name =="StarCoin")
        {
            GameManager.Instance.totalStarCoin++;
            UIManager.Instance.SetTotalStarCoinText();
        }


        for (int i = 0; i < 5; i++)
        {
            Vector3 scale = this.transform.localScale;
            scale.x += 0.2f;
            scale.y += 0.2f;
            this.transform.localScale = scale;
        }
        yield return new WaitForSeconds(0.01f);
        this.gameObject.SetActive(false);
    }
}
