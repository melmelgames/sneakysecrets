using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    public static GameOverWindow instance;

    public Button restartBtn;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public void RestartBtnOnClick()
    {
        SceneManager.LoadScene("GameScene");
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

    public static void HideStatic()
    {
        instance.Hide();
    }
}
