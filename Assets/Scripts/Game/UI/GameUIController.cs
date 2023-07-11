using System;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GamePlay gamePlay;
    public GameUI workUI, timeUI;
    public GameObject gameOverUI, winUI;

    private void Start()
    {
        if (gamePlay == null)
            gamePlay = GamePlay.instance;
        gamePlay.workEvent.AddListener(UpdateScore);
        gamePlay.addTimeEvent.AddListener(UpdateTime);
        GamePlay.onGameLost.AddListener(ShowGameOverUI);
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
        this.workUI.UpdateScore(score);
    }
}