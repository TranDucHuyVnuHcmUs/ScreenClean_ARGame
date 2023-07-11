using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameConfig config;
    [SerializeField] private GameStateIterator stateIterator;
    [SerializeField] private GameStateGeneralRenderer stateRenderer;
    

    private void Awake()
    {
        Instance = this;
        stateIterator = new GameStateIterator(config.stateList);
    }

    private void Start()
    {
        NextState();
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
