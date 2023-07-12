using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameStateGeneralRenderer : MonoBehaviour
{
    public GameCountdownStateRenderer countdownStateRenderer;
    public GamePlayStateRenderer playStateRenderer;
    public GameHandGestureActionStateRenderer handGestureActionStateRenderer;
    public GameLastLevelStateRenderer lastLevelStateRenderer;
    public GameWinStateRenderer winStateRenderer;

    [SerializeField] private GameState _currentState;
    [SerializeField] private Type _currentGameStateDerivedType;
    
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

    public void RenderState(GameState state, Type derivedType)
    {
        _currentState = state;
        _currentGameStateDerivedType = derivedType;

        renderers[derivedType].RenderState(state);
    }

    public void RenderState<T>(T state) where T: GameState
    {
        _currentState = state;
        _currentGameStateDerivedType = typeof(T);

        Type stateType = typeof(T);
        renderers[stateType].RenderState(state);
    }

    internal void UnrenderCurrentState()
    {
        if (_currentState == null) return;
        UnrenderState(_currentState, _currentGameStateDerivedType);
    }

    private void UnrenderState(GameState currentState, Type currentType)
    {
        renderers[currentType].UnrenderState(currentState);
    }
}
