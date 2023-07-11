using System;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public GameUI timeUI;
    public GameObject gameOverUI, winUI;

    private void Start()
    {
        GamePlay.addTimeEvent.AddListener(UpdateTime);
        GamePlay.onGameLost.AddListener(ShowGameOverUI);
    }


    private void ShowGameOverUI()
    {
        gameOverUI.SetActive(true);
    }

    private void UpdateTime(float time)
    {
        timeUI.UpdateScore(time);
    }

}