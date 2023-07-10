using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float score;
    public float time;

    public GameConfig config;
    [SerializeField] private GameStateIterator stateIterator;
    [SerializeField] private GameStateGeneralRenderer stateRenderer;
    
    //public int dirtCount = 0;
    //public GameObject dirtGroup;

    [Header("Events")]
    public UnityEvent nextLevelEvent, winEvent, gameOverEvent;
    public FloatUnityEvent addScoreEvent, addTimeEvent;

    private void Awake()
    {
        Instance = this;
        stateIterator = new GameStateIterator(config.stateList);
        InitEvents();
    }

    private void InitEvents()
    {
        addScoreEvent = new FloatUnityEvent();
        addTimeEvent = new FloatUnityEvent();
    }

    private void Start()
    {
        //dirtCount = dirtGroup.GetComponentsInChildren<DirtCube>().Length;
        NextState();
    }

    public void RemoveDirt()
    {
        //--dirtCount;
        //if (dirtCount == 0)
        //{
        //    this.Win();
        //}
    }

    public void AddScore(float addedScore)
    {
        score += addedScore;
        addScoreEvent.Invoke(score);
    }


    #region events

    public void GameOver()
    {
        gameOverEvent.Invoke();
        //gameOverUI.SetActive(true);
    }

    public void NextLevel()
    {
        nextLevelEvent.Invoke();
    }

    public void Win()
    {

        winEvent.Invoke();
        //winUI.SetActive(true);
    }

    #endregion


    private void Update()
    {
        time += Time.deltaTime;
        addTimeEvent.Invoke(time);
        //timeUI.UpdateScore(score);
    }

    internal static void NextState()
    {
        GameManager.Instance.stateRenderer.UnrenderCurrentState();
        if (!GameManager.Instance.stateIterator.EndOfList())
        {
            var next = GameManager.Instance.stateIterator.Next();
            var type = GameStateUtils.FindDerivedType(next);
            GameManager.Instance.stateRenderer.RenderState(next, type);
        }    
    }
}
