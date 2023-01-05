using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private float ScoreF = 0f;
    private int Score = 0;
    public Text ScoreText;
    private float Frequency = 1f;

    private float Attempt = 0;
    public Text AttemptText;

    private void Update()
    {
        ScoreF = ScoreF + (Frequency * Time.deltaTime);
        Score = ((int)ScoreF);
        Attempt = WorldInfo.GetAttempt();
        ScoreText.text = "Score:" + Score;
        AttemptText.text = "Attempt:" + Attempt;
    }
}
