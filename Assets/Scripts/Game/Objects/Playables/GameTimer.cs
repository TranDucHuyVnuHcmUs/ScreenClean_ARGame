using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class GameTimer : MonoBehaviour

{
    public FloatUnityEvent elapsedTimeUpdateEvent;
    public UnityEvent onTimeOver;
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        elapsedTimeUpdateEvent = new FloatUnityEvent();
    }

    internal void StartCountdown(float time)
    {
        StartCoroutine(CountdownCoroutine(time));
    }

    private IEnumerator CountdownCoroutine(float time)
    {
        float elapsedTime = time;
        while (elapsedTime > 0)
        {
            yield return new WaitForEndOfFrame();
            if (!isPaused)
            {
                elapsedTime -= Time.deltaTime;
                elapsedTimeUpdateEvent.Invoke(elapsedTime);
            }
        }
        onTimeOver.Invoke();
    }

    internal void PauseCountdown() { isPaused = true; }
    internal void ResumeCountdown() { isPaused = false; }   
}