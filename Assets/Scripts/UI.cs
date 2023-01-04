using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private int Score = 0;
    public Text ScoreText;

    private int Attempt = 0;
    public Text AttemptText;

    private void Update()
    {
        Attempt = WorldInfo.GetAttempt();
        ScoreText.text = "Score:" + Score;
        AttemptText.text = "Attempt:" + Attempt;
    }
}
