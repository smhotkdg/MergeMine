using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSimulation : MonoBehaviour
{
    public void EndAnim()
    {
        this.gameObject.SetActive(false);
    }
    public void DrillUIStart()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.StartDrillGame();
    }
    public void DrillUIEnd()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.EndDrillGame_Anim();
    }

    public void EnterSound()
    {
        SoundsManager.Instance.EnterSound();
    }
    public void OpenSound()
    {
        SoundsManager.Instance.OpenSound();
    }
    public void SetDrillGame()
    {
        //StartDillGame
        UIManager.Instance.DrillGameStart();
    }
}
