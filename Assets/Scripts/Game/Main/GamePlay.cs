
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;



public class GamePlay : MonoBehaviour
{
    public static GamePlay instance;

    public static UnityEvent onGameStart, onGamePaused, onGameResume, onGameWon, onGameLost;

    public GamePlayMaker gameMaker;

    public float works = 0;         // sorry, don't know better word. haha... Basically level complete when works 'done', works = 0.
    public float time;

    [Header("Events")]
    public FloatUnityEvent workEvent, addTimeEvent;

    private void Awake()
    {
        if (instance != null) throw new Exception("There is already one GamePlay instance!");
        instance = this;

        onGameStart = new UnityEvent();
        onGamePaused = new UnityEvent();
        onGameResume = new UnityEvent();
        onGameWon = new UnityEvent();
        onGameLost = new UnityEvent();
    }

    private void Start()
    {
        InitEvents();
    }
    private void InitEvents()
    {
        workEvent = new FloatUnityEvent();
        addTimeEvent = new FloatUnityEvent();
    }

    #region main

    public static void AddWork(float workValue) { instance.works += workValue; }

    public static void MakeNewGame(GamePlayState state)
    {
        instance.gameMaker.MakeGame(state);
    }

    public static void Work(float workValue)
    {
        instance.works -= workValue;
        instance.workEvent.Invoke(workValue);
        if (instance.works <= 0)
        {
            Win();
        }
    }

    public static void GameOver()
    {
        onGameLost.Invoke();
    }

    public static void Win()
    {
        onGameWon.Invoke();
    }

    #endregion


    private void Update()
    {
        time += Time.deltaTime;
        addTimeEvent.Invoke(time);
        //timeUI.UpdateScore(score);
    }

    internal static void CleanGame()
    {
        instance.gameMaker.CleanGame();
        instance.time = 0;
        instance.works = 0;
    }

    internal static void StartGame()
    {
        onGameStart.Invoke();
    }
}