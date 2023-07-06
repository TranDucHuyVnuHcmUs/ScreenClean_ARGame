using UnityEngine;

public class GameWinStateRenderer : GameStateRenderer<GameWinState>
{
    public GameObject winUI;
    public override void RenderState(GameWinState state)
    {
        winUI.SetActive(true);
    }

    public override void UnrenderState(GameWinState state)
    {
        winUI.SetActive(false);
    }
}