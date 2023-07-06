using System;
using UnityEngine;
using UnityEngine.UI;

public class GameHandGestureActionStateRenderer : GameStateRenderer<GameHandGestureActionState>
{
    public GameObject ui;
    public Text label;

    public override void RenderState(GameHandGestureActionState state)
    {
        ui.SetActive(true);
        label.text = HandGestureInputSystemUtils.HandGestureActionToString(state.actionToContinue);
        HandGestureInputSystem.ListenToActionStartEvent(state.actionToContinue, NextState);
    }

    private void NextState(HandGestureInputEventArgs arg0)
    {
        GameManager.NextState();
    }

    public override void UnrenderState(GameHandGestureActionState state)
    {
        HandGestureInputSystem.UnregisterToActionStartEvent(state.actionToContinue, NextState);
        ui.SetActive(false);
    }
}