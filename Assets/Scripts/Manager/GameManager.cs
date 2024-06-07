using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text startMessage;
    public Text gameOverMessage;
    public Slider overchargeSlider;
    public int score = 0;
    private int multiplier = 1;
    private float overcharge = 0f;
    private float maxOvercharge = 100f;
    private bool gameStarted = false;
    private int highScore = 0;

    void Start()
    {
        overchargeSlider.maxValue = 1;
        //ShowStartMessage();
        StartGame();
    }

    void Update()
    {
        overchargeSlider.value = overcharge/maxOvercharge;

        if (overcharge > 0)
        {
            overcharge -= Time.deltaTime * 5;
        }
        else
        {
            multiplier = 1;
        }

        if (overcharge >= maxOvercharge)
        {
            GameOver();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        score = 0;
        overcharge = 0;
        multiplier = 1;
        startMessage.gameObject.SetActive(false);
        gameOverMessage.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameStarted = false;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        gameOverMessage.text = "Game Over! Press Enter to Restart\nHigh Score: " + highScore;
        gameOverMessage.gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().PlayGameOverSound();
        Time.timeScale = 0;
    }

    public void AddScore(int points)
    {
        score += points * multiplier;
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseOvercharge(float amount)
    {
        Debug.Log("Overcharge: " + overcharge);
        overcharge += amount;
        if (overcharge < maxOvercharge)
        {
            multiplier++;
        }
        else
        {
            overcharge = maxOvercharge;
        }
    }

    public bool IsOvercharged()
    {
        return overcharge >= maxOvercharge;
    }

    void ShowStartMessage()
    {
        startMessage.gameObject.SetActive(true);
        gameOverMessage.gameObject.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        startMessage.text = "Press Enter to Start\nHigh Score: " + highScore;
        Time.timeScale = 0;
    }
}
