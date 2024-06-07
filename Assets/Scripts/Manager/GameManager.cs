using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Slider overchargeSlider;
    public int score = 0;
    private int multiplier = 1;
    private float overcharge = 0f;
    private float maxOvercharge = 100f;
    private bool gameStarted = false;
    private int highScore = 0;

    void Start()
    {
        overchargeSlider.maxValue = maxOvercharge;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        StartGame();
    }

    void Update()
    {
        if (!gameStarted)
            return;

        overchargeSlider.value = overcharge;

        if (overcharge > 0)
        {
            overcharge -= Time.deltaTime * 5; // Diminution de la surtension au fil du temps
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
        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("GameOverScene");
    }

    public void AddScore(int points)
    {
        score += points * multiplier;
        scoreText.text = "Score: " + score.ToString();
    }

    public void IncreaseOvercharge(float amount)
    {
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
}
