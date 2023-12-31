using System;
using UnityEngine;
using UnityEngine.UI;

public class CountdownStateUI : MonoBehaviour
{
    public GameTimer timer;
    public Text timeText;

    private void Start()
    {
        timer.elapsedTimeUpdateEvent.AddListener(ChangeTimeLabel);
    }

    private void ChangeTimeLabel(float arg0)
    {
        timeText.text =  ((int)Math.Floor(arg0) + 1).ToString();
    }
}