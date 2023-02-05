using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    private float ScoreF = 0f;
    private int Score = 0;
    public TMP_Text ScoreText;
    private float Frequency = 1f;
    private float Attempt = 0;
    public TMP_Text AttemptText;
    private float HighScoreF = 0;
    private int HighScore = 0;
    public TMP_Text HighScoreText;
    public GameObject PauseMenuUI;
    public Slider LevelSlider;
    public static bool GameIsPause = false;
    public TMP_Text SeedText;
    public TMP_Text FinAttemptText;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause) { Resume(); } else { Pause(); }
        }
        if(WorldInfo.HasDied ==  true) {Attempt+= 1;
           WorldInfo.HasDied = false;
            if (ScoreF > HighScoreF) { HighScoreF= ScoreF; }
            ScoreF = 0;
            HighScore = (int)HighScoreF;
            HighScoreText.text = "HIGHSCORE:" + HighScore;   
        }
        LevelSlider.value = Score * 2;
        ScoreF = ScoreF + (Frequency * Time.deltaTime);
        Score = ((int)ScoreF);
        ScoreText.text = "Score:" + Score;
        AttemptText.text = "Attempt:" + Attempt;

        if (WorldInfo.GameFin) {
            if (WorldInfo.GetSeed() != null) {SeedText.text = "Seed:" + WorldInfo.GetSeed(); }
            FinAttemptText.text = "Completed in: " + Attempt + " Attempts";
        }
    }

    public void Resume() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    private void Pause() { 
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause= true;
    }

    public void LoadMenu() {
        SceneManager.LoadScene(0);
        
    }

    public void Replay() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    public void Quit() { Application.Quit(); }
}
