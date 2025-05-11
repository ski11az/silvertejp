using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        GameManager.ScoreChanged += UpdateEarningsText;
    }

    private void OnDisable()
    {
        GameManager.ScoreChanged -= UpdateEarningsText;
    }

    private void UpdateEarningsText(int currentScore)
    {
        scoreText.text = "$" + currentScore;
    }
}
