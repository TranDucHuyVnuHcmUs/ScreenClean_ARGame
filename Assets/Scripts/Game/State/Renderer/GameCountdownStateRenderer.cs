using UnityEngine;

public class GameCountdownStateRenderer : GameStateRenderer<GameCountdownState>
{
    public CountdownStateUI ui;
    public GameTimer gameTimer;     //a MonoBehaviour script

    private void Start()
    {
        gameTimer.timeOverEvent.AddListener(GameManager.NextState);
    }

    public override void RenderState(GameCountdownState state)
    {
        ui.gameObject.SetActive(true);
        gameTimer.StartCountdown(state.time);
        UnityEngine.Debug.Log("Countdown state renderer");
    }

    public override void UnrenderState(GameCountdownState state)
    {
        ui.gameObject.SetActive(false);
        UnityEngine.Debug.Log("Countdown state renderer ended.");
    }
}