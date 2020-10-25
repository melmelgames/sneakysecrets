using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    public Text scoreText;
    public Text enemiesKilledText;
    public Slider healthBar;
    public static ScoreWindow instance;

    private void Start()
    {
        instance = this;
        Hide();
    }
    private void Update()
    {
        UpdateScore();
        UpdateHealth();
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

    private void UpdateScore()
    {
        scoreText.text = "SCORE: " + PlayerController.instance.GetScore().ToString();
        enemiesKilledText.text = "KILLS: " + PlayerController.instance.GetKills().ToString();
    }

    public void UpdateHealth()
    {
        healthBar.value = PlayerController.instance.GetHealth();
    }
}
