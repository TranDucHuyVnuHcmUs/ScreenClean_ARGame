using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateGeneralRenderer : MonoBehaviour
{
    public GameCountdownStateRenderer countdownStateRenderer;
    public GamePlayStateRenderer playStateRenderer;
    public GameHandGestureActionStateRenderer handGestureActionStateRenderer;
    public GameLastLevelStateRenderer lastLevelStateRenderer;
    public GameWinStateRenderer winStateRenderer;

    [SerializeField] private GameState _currentState;
    
    private Dictionary<Type, GameStateRenderer> renderers;

    private void Awake()
    {
        renderers = new Dictionary<Type, GameStateRenderer>()
        {
            { typeof(GameCountdownState), countdownStateRenderer },
            { typeof(GamePlayState), playStateRenderer },
            { typeof(GameHandGestureActionState), handGestureActionStateRenderer },
            { typeof(GameLastLevelPlayState), lastLevelStateRenderer },
            { typeof(GameWinState), winStateRenderer },
        };
    }

    public void RenderState<T>(T state) where T: GameState
    {
        Type stateType = typeof(T);
        renderers[stateType].RenderState(state);
    }

    internal void UnrenderCurrentState()
    {
        if (_currentState == null) return;
        UnrenderState(_currentState);
    }

    private void UnrenderState<T>(T currentState) where T: GameState
    {
        renderers[typeof(T)].RenderState(currentState);
    }
}
