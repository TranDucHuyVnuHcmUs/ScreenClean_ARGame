using System;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GameManager gameManager;
    public GameUI scoreUI, timeUI;
    public GameObject gameOverUI, winUI;

    private void Start()
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;
        gameManager.addScoreEvent.AddListener(UpdateScore);
        gameManager.addTimeEvent.AddListener(UpdateTime);
        gameManager.gameOverEvent.AddListener(ShowGameOverUI);
        gameManager.winEvent.AddListener(ShowGameWinUI);
    }

    private void ShowGameWinUI()
    {
        winUI.SetActive(true);
    }

    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    private void UpdateTime(float time)
    {
        timeUI.UpdateScore(time);
    }

    internal void UpdateScore(float score)
    {
        this.scoreUI.UpdateScore(score);
    }
}