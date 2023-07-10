using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("InGameUI")]
    [SerializeField] private TMP_Text gemsCollectedText;

    [Header("GameOverUI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelClearedImg;
    [SerializeField] private GameObject tryAgainImg;
    [SerializeField] private TMP_Text gameOverGemsText;

    private void OnEnable()
    {
        EventsManager.Instance.OnPickupCoin += UpdateCollectedGemsText;
        EventsManager.Instance.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnPickupCoin -= UpdateCollectedGemsText;
        EventsManager.Instance.OnGameOver -= GameOver;
    }


    private void UpdateCollectedGemsText(object sender, int _gems)
    {
        gemsCollectedText.text = _gems.ToString();
        gameOverGemsText.text = _gems.ToString();
    }

    private void GameOver(object sender, bool _hasFinishedGame)
    {
        if (_hasFinishedGame)
        {
            levelClearedImg.SetActive(true);
            tryAgainImg.SetActive(false);
        }
        else
        {
            levelClearedImg.SetActive(false);
            tryAgainImg.SetActive(true);
        }

        gameOverPanel.SetActive(true);
    }
}
