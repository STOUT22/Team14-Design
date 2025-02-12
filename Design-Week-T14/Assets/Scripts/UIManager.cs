using UnityEngine;
using TMPro;
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
    public void OnPlayerDeath(string playerTag)
    {
        if (playerTag == "Player1")
        {
            AddPlayer2Score(1); // Player 2 gets 1 point when Player 1 dies
        }
        else if (playerTag == "Player2")
        {
            AddPlayer1Score(1); // Player 1 gets 1 point when Player 2 dies
        }
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

            // Once extra time starts, change kill point value to 3
            StartCoroutine(ChangeKillPointsToThree());
        }
    }
    private IEnumerator ChangeKillPointsToThree()
    {
        yield return new WaitForSeconds(0f);  // Ensure we start right after the countdown

        // Change the point system for killing to 3 points after extra time
        AddPlayer1Score(3);  // Player 1 now gets 3 points for kills
        AddPlayer2Score(3);  // Player 2 now gets 3 points for kills
    }
    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update health of the player
    public void UpdatePlayerHealth(string playerTag, int health)
    {
        if (playerTag == "Player1")
        {
            player1HealthBar.value = health;
        }
        else if (playerTag == "Player2")
        {
            player2HealthBar.value = health;
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
