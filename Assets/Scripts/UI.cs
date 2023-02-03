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

    public GameObject PauseMenuUI;

    public static bool GameIsPause = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause) { Resume(); } else { Pause(); }
        }
        if(WorldInfo.HasDied ==  true) {Attempt+= 1;
           WorldInfo.HasDied = false;
            ScoreF = 0;
        }
        ScoreF = ScoreF + (Frequency * Time.deltaTime);
        Score = ((int)ScoreF);
        ScoreText.text = "Score:" + Score;
        AttemptText.text = "Attempt:" + Attempt;
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

    public void Quit() { Application.Quit(); }
}
