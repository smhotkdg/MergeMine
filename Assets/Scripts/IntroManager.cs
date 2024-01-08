using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text progrssText;
    public Image PrograssImage;
    bool start = false;
    void Start()
    {
        StartCoroutine(LevelStart());
    }

    // Update is called once per frame
    IEnumerator LevelStart()
    {
        yield return new WaitForSeconds(0.2f);
        LoadLevel("MergeMine");
    }
    void Update()
    {
        
    }
    public void LoadScenes()
    {
        SceneManager.LoadScene("MergeMine");
    }

    public void LoadLevel(string nameScene)
    {
        if (start == false)
        {            

            StartCoroutine(LoadAsynchronously(nameScene));
            start = true;
        }
    }


    IEnumerator LoadAsynchronously(string nameScen)
    {
        yield return new WaitForSeconds(0.1f);
        AsyncOperation opertation = SceneManager.LoadSceneAsync(nameScen);
        //AsyncOperation opertation =  Application.LoadLevelAsync(0);

        while (!opertation.isDone)
        {
            float progress = Mathf.Clamp01(opertation.progress / .9f);

            if (progress * 100f > 100)
            {
                progrssText.text = 100.ToString("N0") + "%";
                PrograssImage.fillAmount = 1;
            }
            else
            {
                float temp = progress * 100f;
                progrssText.text = temp.ToString("N0") + "%";
                PrograssImage.fillAmount = progress;

            }
            yield return null;
        }
    }

    float GetResolution(int width, int height)
    {
        float scRatio = (float)width / (float)height;
        float scRound = Mathf.Round(scRatio * 100.0f);
        scRound = scRound / 100f;
        return scRound;
    }
}
