using UnityEngine;

public class GameTimerMaker : GameConcreteMaker<GameTimerData>
{
    internal override void InitObject(GameObject newObj, GameTimerData agentData)
    {
        var gameTimerCtrl = newObj.GetComponent<GameTimerController>();
        gameTimerCtrl.time = agentData.time;
        GamePlay.onGameStart.AddListener(gameTimerCtrl.StartTimer);
        GamePlay.onGamePaused.AddListener(gameTimerCtrl.PauseTimer);
        GamePlay.onGameResume.AddListener(gameTimerCtrl.ResumeTimer);
        gameTimerCtrl.ListenToOnTimerCountdownEnd(GamePlay.GameOver);
    }
}