using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GoldManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Image MyImage;
    private bool MyFlag;
    private bool MyFlag_StartEndGold;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        MyImage = GetComponent<Image>();
        StartCoroutine(EndGold());
        MyFlag_StartEndGold = false;
        MyFlag = false;
    }

    IEnumerator EndGold()
    {     
        yield return new WaitForSeconds(2f);
        if(MyFlag == false)
        {
            MyFlag_StartEndGold = true;
            Color32 myColor = new Color32(255, 255, 255, 255);
            float delta = 255 / 30;
            for (int i = 0; i < 30; i++)
            {
                float alpha = 255 - (delta * (i + 1));
                myColor = new Color32(255, 255, 255, (byte)alpha);
                MyImage.color = myColor;
                yield return new WaitForSeconds(0.02f);
            }
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);            
        }
            
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickGold()
    {
        if(MyFlag == false && MyFlag_StartEndGold == false)
        {
            StartCoroutine(EndGoldClick());
            MyFlag = true;
        }     
    }
    IEnumerator EndGoldClick()
    {   
        Color32 myColor = new Color32(255, 255, 255, 255);
        float delta = 255 / 10;
        for (int i = 0; i < 10; i++)
        {
            float alpha = 255 - (delta * (i + 1));
            myColor = new Color32(255, 255, 255, (byte)alpha);
            MyImage.color = myColor;
            yield return new WaitForSeconds(0.02f);
        }
        this.gameObject.SetActive(false);        
    }
}
