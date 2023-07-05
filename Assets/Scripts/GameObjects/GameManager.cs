using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float score;
    public float time;
    public GameUI scoreUI, timeUI;
    public GameObject gameOverUI, winUI;
    public bool isCleaning = false;

    public int dirtCount = 0;
    public GameObject dirtGroup;

    public UnityEvent startCleaningEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dirtCount = dirtGroup.GetComponentsInChildren<DirtCube>().Length;
    }

    public void RemoveDirt()
    {
        --dirtCount;
        if (dirtCount == 0)
        {
            this.Win();
        }
    }

    public void AddScore(float addedScore)
    {
        score += addedScore;
        scoreUI.UpdateScore(score);
    }

    public void StartCleaning()
    {
        isCleaning = true;
        startCleaningEvent.Invoke();
    }

    #region events

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Win()
    {
        winUI.SetActive(true);
    }

    #endregion


    private void Update()
    {
        time += Time.deltaTime;
        timeUI.UpdateScore(score);
    }
}
