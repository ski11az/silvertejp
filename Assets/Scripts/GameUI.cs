using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI deliverHintText;
    [SerializeField] TextMeshProUGUI sellText;

    private void OnEnable()
    {
        GameManager.ScoreChanged += UpdateEarningsText;
        GameManager.AllShardsUsed += ShowHint;
    }

    private void OnDisable()
    {
        GameManager.ScoreChanged -= UpdateEarningsText;
        GameManager.AllShardsUsed -= ShowHint;
    }

    private void UpdateEarningsText(int currentScore)
    {
        scoreText.text = "$" + currentScore;

        if (currentScore > 0) StartCoroutine(Co_FlashText(sellText));
    }

    private void ShowHint()
    {
        StartCoroutine(Co_FlashText(deliverHintText));
    }

    private IEnumerator Co_FlashText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
    }
}
