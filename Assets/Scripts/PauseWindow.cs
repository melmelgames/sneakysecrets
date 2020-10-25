using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour
{
    public static PauseWindow instance;

    public Button resumeBtn;
    public Button restartBtn;

    private void Awake()
    {
        instance = this;
        HidePauseWindow();
    }


    public void RestartBtnOnClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ResumeBtnOnClick()
    {
        HidePauseWindow();
        GameManager.ResumeGameStatic();
    }

    private void ShowPauseWindow()
    {
        gameObject.SetActive(true);
    }

    private void HidePauseWindow()
    {
        gameObject.SetActive(false);
    }

    public static void ShowPauseWindowStatic()
    {
        instance.ShowPauseWindow();
    }

    public static void HidePauseWindowStatic()
    {
        instance.HidePauseWindow();
    }
}
