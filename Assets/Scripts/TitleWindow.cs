using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleWindow : MonoBehaviour
{
    public static TitleWindow instance;

    public Button startBtn;

    private void Awake()
    {
        instance = this;
    }


    public void StartBtnOnClick()
    {
        GameManager.ResumeGameStatic();
        GameManager.StartGame();
        TutorialWindow.ShowStatic();
        instance.Hide();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void ShowStatic()
    {
        instance.Show();
    }
}
