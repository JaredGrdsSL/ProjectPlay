using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    private int score = 0;
    private int highscore = 0;
    private float initialYPosition; // Initial Y position of the player

    // Reference to the player GameObject (assuming it's tagged as "Player")
    private GameObject player;

    // Key to save and load highscore from PlayerPrefs
    private string highscoreKey = "Highscore";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag
        if (player != null)
        {
            initialYPosition = player.transform.position.y; // Get initial Y position
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player is tagged as 'Player'.");
        }

        // Load the highscore from PlayerPrefs
        highscore = PlayerPrefs.GetInt(highscoreKey, 0);
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the vertical distance fallen by the player
            float distanceFallen = initialYPosition - player.transform.position.y;

            // Update score based on the distance fallen
            int distanceAsScore = Mathf.FloorToInt(distanceFallen); // Convert distance to integer for score
            if (distanceAsScore > score)
            {
                score = distanceAsScore;
                UpdateScore();
            }
        }
    }

    // Function to update score display
    void UpdateScore()
    {
        UpdateScoreText();

        // Update highscore if the current score surpasses it
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt(highscoreKey, highscore); // Save highscore to PlayerPrefs
            highscoreText.text = "highscore: " + highscore.ToString();
        }
    }

    // Function to update score text
    void UpdateScoreText()
    {
        scoreText.text = "score: " + score.ToString();
        highscoreText.text = "highscore: " + highscore.ToString();
    }
}