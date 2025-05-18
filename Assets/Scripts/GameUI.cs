using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI deliverHintText;
    [SerializeField] TextMeshProUGUI sellText;

    [SerializeField] Image vasePreview;
    [SerializeField] Vase[] vasePrefabs;
    [SerializeField] Sprite[] vaseSprites;

    private void OnEnable()
    {
        GameManager.ScoreChanged += UpdateEarningsText;
        GameManager.AllShardsUsed += ShowHint;
        GameManager.StartedVase += SetPreviewImage;
    }

    private void OnDisable()
    {
        GameManager.ScoreChanged -= UpdateEarningsText;
        GameManager.AllShardsUsed -= ShowHint;
        GameManager.StartedVase -= SetPreviewImage;
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

    private void SetPreviewImage(Vase vase)
    {
        int index = Array.IndexOf(vasePrefabs, vase);
        vasePreview.sprite = vaseSprites[index];
        vasePreview.SetNativeSize();
    }
}
