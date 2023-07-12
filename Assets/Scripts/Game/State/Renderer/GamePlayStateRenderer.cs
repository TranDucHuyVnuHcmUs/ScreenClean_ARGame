using System.Diagnostics;
using UnityEngine;

public class GamePlayStateRenderer : GameStateRenderer<GamePlayState>
{
    private void Start()
    {
        GamePlay.onGameWon.AddListener(GameManager.NextState);
    }

    public override void RenderState(GamePlayState state)
    {
        UnityEngine.Debug.Log("Play state renderer");
        GamePlay.MakeNewGame(state);
        GamePlay.StartGame();
    }

    public override void UnrenderState(GamePlayState state)
    {
        UnityEngine.Debug.Log("Play state renderer");
        GamePlay.CleanGame();
    }
}