using UnityEngine;

public class GameLastLevelStateRenderer : GameStateRenderer<GameLastLevelPlayState>
{
    public GameObject gameContent;

    public override void RenderState(GameLastLevelPlayState state)
    {
        gameContent.SetActive(true);
    }

    public override void UnrenderState(GameLastLevelPlayState state)
    {
        gameContent.SetActive(false);
    }
}