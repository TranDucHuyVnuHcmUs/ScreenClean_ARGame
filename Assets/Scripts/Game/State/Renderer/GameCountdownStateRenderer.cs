using UnityEngine;

public class GameCountdownStateRenderer : GameStateRenderer<GameCountdownState>
{
    public CountdownStateUI ui;
    public GameTimer gameTimer;     //a MonoBehaviour script

    public override void RenderState(GameCountdownState state)
    {
        ui.gameObject.SetActive(true);
        gameTimer.timeOverEvent.AddListener(GameManager.NextState);
        gameTimer.StartCountdown(state.time);
        UnityEngine.Debug.Log("Countdown state renderer");
    }

    public override void UnrenderState(GameCountdownState state)
    {
        gameTimer.timeOverEvent.RemoveListener(GameManager.NextState);
        ui.gameObject.SetActive(false);
        UnityEngine.Debug.Log("Countdown state renderer");
    }
}