using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static bool gameOver;
    private static bool isPaused;
    private static bool gameOn;
    private PlayerController playerController;

    private void Awake()
    {
        instance = this;
        
        gameOver = false;
        isPaused = false;
        gameOn = false;
        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        playerController = PlayerController.instance;
        PauseGameTutorial();
    }

    private void Update()
    {
        
        int health = playerController.GetHealth();
        if (health <= 0)
        {
            GameOver();
        }
        
        if (Input.GetKeyDown(KeyCode.P) && gameOn)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else if (!isPaused)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        PauseWindow.ShowPauseWindowStatic();
        Time.timeScale = 0.0f;
    }

    public void PauseGameTutorial()
    {
        Time.timeScale = 0.0f;
    }

    private void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver = true;
        gameOn = false;
        GameOverWindow.ShowStatic();
    }

    public static void GameOverStatic()
    {
        instance.GameOver();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gameOver = false;
        isPaused = false;
        PauseWindow.HidePauseWindowStatic();
    }

    public void ResumeGameTutorial()
    {
        Time.timeScale = 1.0f;
    }

    public static void ResumeGameStatic()
    {
        instance.ResumeGame();
        ScoreWindow.ShowStatic();
    }

    public static void StartGame()
    {
        gameOn = true;
    }

}
