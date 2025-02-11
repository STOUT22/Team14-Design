using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    // Player 1 UI Elements
    public TextMeshProUGUI player1ScoreText;  // UI TextMeshPro component for Player 1 score
    public Slider player1HealthBar; // UI Slider element for Player 1 health bar
    public int player1InitialHealth = 100;  // Player 1's initial health

    // Player 2 UI Elements
    public TextMeshProUGUI player2ScoreText;  // UI TextMeshPro component for Player 2 score
    public Slider player2HealthBar; // UI Slider element for Player 2 health bar
    public int player2InitialHealth = 100;  // Player 2's initial health

    public TextMeshProUGUI timerText;  // UI TextMeshPro component for timer
    private int player1Score = 0;  // Player 1's score
    private int player2Score = 0;  // Player 2's score

    private float countdownTime = 150f; // 2 minutes 30 seconds in seconds
    private float extraTime = 60f;  // 1 minute additional time
    private float currentTime;
    private bool extraTimeAdded = false;

    void Start()
    {
        currentTime = countdownTime;

        // Set up health bars and initial values
        player1HealthBar.maxValue = player1InitialHealth;
        player2HealthBar.maxValue = player2InitialHealth;
        player1HealthBar.value = player1InitialHealth;
        player2HealthBar.value = player2InitialHealth;

        UpdateScoreText();
        UpdateTimerText();

        StartCoroutine(TimerCountdown());
    }

    void Update()
    {
        // You can include any other game logic here for managing player health, etc.
    }

    private IEnumerator TimerCountdown()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // Decrease time by frame
            UpdateTimerText();
            yield return null;
        }

        // When the timer reaches 0, add extra time if it hasn't been added already
        if (!extraTimeAdded)
        {
            extraTimeAdded = true;
            currentTime = extraTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Player 1 Health Management
    public void UpdatePlayer1Health(int healthChange)
    {
        player1HealthBar.value += healthChange;

        // Ensure health doesn't go below 0 or above max
        if (player1HealthBar.value <= 0)
        {
            player1HealthBar.value = 0;
            // Trigger Player 1 game over logic here
        }
        else if (player1HealthBar.value >= player1HealthBar.maxValue)
        {
            player1HealthBar.value = player1HealthBar.maxValue;
        }
    }

    // Player 2 Health Management
    public void UpdatePlayer2Health(int healthChange)
    {
        player2HealthBar.value += healthChange;

        // Ensure health doesn't go below 0 or above max
        if (player2HealthBar.value <= 0)
        {
            player2HealthBar.value = 0;
            // Trigger Player 2 game over logic here
        }
        else if (player2HealthBar.value >= player2HealthBar.maxValue)
        {
            player2HealthBar.value = player2HealthBar.maxValue;
        }
    }

    // Add score for Player 1
    public void AddPlayer1Score(int points)
    {
        player1Score += points;
        UpdateScoreText();
    }

    // Add score for Player 2
    public void AddPlayer2Score(int points)
    {
        player2Score += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        player1ScoreText.text = "Score: " + player1Score.ToString();
        player2ScoreText.text = "Score: " + player2Score.ToString();
    }
}
