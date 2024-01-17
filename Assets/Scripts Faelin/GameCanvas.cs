using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvas : MonoBehaviour {
    public TextMeshProUGUI scoreTextDied;
    public TextMeshProUGUI highscoreTextDied;
    public TextMeshProUGUI scoreTextWon;
    public TextMeshProUGUI highscoreTextWon;
    public TextMeshProUGUI energysTextWon;
    public TextMeshProUGUI energysTextDied;
    private int score = 0;
    private int highscore = 0;
    public float initialYPosition;
    public int distanceAsScore;

    private GameObject player;
    private AudioManager audioManager;

    private string highscoreKey = "Highscore";

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        if (player != null) {
            initialYPosition = player.transform.position.y;
        }
        else {
            Debug.LogError("Player not found! Make sure the player is tagged as 'Player'.");
        }
        highscore = PlayerPrefs.GetInt(highscoreKey, 0);
    }

    void Update() {
        if (player != null) {
            float distanceFallen = Mathf.Abs(initialYPosition) - Mathf.Abs(player.transform.position.y);

            distanceAsScore = Mathf.FloorToInt(Mathf.Abs(distanceFallen) * PlayerPrefs.GetFloat("ScoreMultiplier", .7f));
            if (distanceAsScore > score) {
                score = distanceAsScore;
            }
        }
    }

    public void UpdateScore() {
        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetInt(highscoreKey, highscore);
        }
        highscoreTextDied.text = "highscore " + highscore.ToString();
        highscoreTextWon.text = "highscore " + highscore.ToString();
        scoreTextDied.text = "score " + score.ToString();
        scoreTextWon.text = "score " + score.ToString();
    }

    public void restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToTitleScreen() {
        audioManager.Stop("LevelTheme");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void playButtonOpenSFX() {
        audioManager.Play("ButtonOpen");
    }

    public void playButtonCloseSFX() {
        audioManager.Play("ButtonClose");
    }
}
