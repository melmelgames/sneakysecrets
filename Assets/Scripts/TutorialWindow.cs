using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWindow : MonoBehaviour
{
    public static TutorialWindow instance;
    private void Awake()
    {
        instance = this;
        Hide();
    }

    private void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Hide();
        }
    }

    private void OnEnable()
    {
        GameManager.instance.PauseGameTutorial();
    }

    private void OnDisable()
    {
        GameManager.ResumeGameStatic();
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
