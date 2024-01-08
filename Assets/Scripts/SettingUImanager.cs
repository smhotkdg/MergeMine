using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingUImanager : MonoBehaviour
{
    public Button BtnKorean;
    public Button BtnEnglish;
    public Button BtnJapan;
    public Button BtnChina_simple;
    public Button BtnChina_trand;
    public Button Vietnam_btn;

    public Button BtnBGM;
    public Button BtnFX;
    // Start is called before the first frame update
    Color EnableColor = new Color32(255, 255, 255, 255);
    Color DisableColor = new Color32(255, 255, 255, 50);
    void Start()
    {
        
    }
    private void OnEnable()
    {
        SetInit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void SetInit()
    {
        if (GameManager.Instance.BGM == true)
        {
            BtnBGM.GetComponent<Image>().color = EnableColor;
        }
        else
        {
            BtnBGM.GetComponent<Image>().color = DisableColor;
        }
        if (GameManager.Instance.Fx == true)
        {
            BtnFX.GetComponent<Image>().color = EnableColor;
        }
        else
        {
            BtnFX.GetComponent<Image>().color = DisableColor;
        }
        SetAllDisable();
        switch (GameManager.Instance.Language_Type)
        {
            case 1:
                BtnKorean.GetComponent<Image>().color = EnableColor;
                break;
            case 2:
                BtnEnglish.GetComponent<Image>().color = EnableColor;
                break;
            case 3:
                BtnJapan.GetComponent<Image>().color = EnableColor;
                break;
            case 4:
                BtnChina_simple.GetComponent<Image>().color = EnableColor;
                break;
            case 5:
                BtnChina_trand.GetComponent<Image>().color = EnableColor;
                break;
            case 6:
                Vietnam_btn.GetComponent<Image>().color = EnableColor;
                break;
             
        }
    }
    void SetAllDisable()
    {
        BtnKorean.GetComponent<Image>().color = DisableColor;
        BtnEnglish.GetComponent<Image>().color = DisableColor;
        BtnJapan.GetComponent<Image>().color = DisableColor;
        BtnChina_simple.GetComponent<Image>().color = DisableColor;
        BtnChina_trand.GetComponent<Image>().color = DisableColor;        
        Vietnam_btn.GetComponent<Image>().color = DisableColor;
    }

    public void ChangeLanguage(int index)
    {
        SetAllDisable();
        GameManager.Instance.Language_Type = index;

        switch (index)
        {
            case 1:
                BtnKorean.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("Kor", "ko");
                break;
            case 2:
                BtnEnglish.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("eng", "en");
                break;
            case 3:
                BtnJapan.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("janpan", "ja");
                break;
            case 4:
                BtnChina_simple.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("chin", "zh_TW");
                break;
            case 5:
                BtnChina_trand.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("chin", "zh_CN");
                break;
            case 6:
                Vietnam_btn.GetComponent<Image>().color = EnableColor;
                I2.Loc.LocalizationManager.SetLanguageAndCode("Indonesian", "vi");
                break;        
        }        
    }
    public void SetBGM()
    {
        if (GameManager.Instance.BGM == true)
        {
            GameManager.Instance.BGM = false;
            BtnBGM.GetComponent<Image>().color = DisableColor;
        }
        else
        {
            GameManager.Instance.BGM = true;
            BtnBGM.GetComponent<Image>().color = EnableColor;
        }
        SoundsManager.Instance.MuteBGM();
    }
    public void SetFX()
    {
        if (GameManager.Instance.Fx == true)
        {
            GameManager.Instance.Fx = false;
            BtnFX.GetComponent<Image>().color = DisableColor;
        }
        else
        {
            GameManager.Instance.Fx = true;
            BtnFX.GetComponent<Image>().color = EnableColor;
        }
    }
}
