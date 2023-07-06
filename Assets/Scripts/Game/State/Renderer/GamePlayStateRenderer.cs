using System.Diagnostics;
using UnityEngine;

public class GamePlayStateRenderer : GameStateRenderer<GamePlayState>
{
    public override void RenderState(GamePlayState state)
    {
        UnityEngine.Debug.Log("Play state renderer");
        throw new System.NotImplementedException();
    }

    public override void UnrenderState(GamePlayState state)
    {
        UnityEngine.Debug.Log("Play state renderer");
        throw new System.NotImplementedException();
    }
}