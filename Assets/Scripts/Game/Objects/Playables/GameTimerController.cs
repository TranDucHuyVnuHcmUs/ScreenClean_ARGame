
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimerController : MonoBehaviour
{
    public GameTimer timer;
    public Text timeLabel;
    public int time;

    private void Start()
    {
        timer.elapsedTimeUpdateEvent.AddListener(ChangeLabel);
    }

    private void ChangeLabel(float arg0)
    {
        timeLabel.text = (arg0).ToString();
    }

    public void StartTimer()
    {
        timer.StartCountdown(time);
    }

    internal void PauseTimer()
    {
        timer.PauseCountdown();
    }
    internal void ResumeTimer() { timer.ResumeCountdown();}

    public void ListenToOnTimerCountdownEnd(UnityAction action)
    {
        timer.onTimeOver.AddListener(action);
    }
}