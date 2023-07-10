using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameTimer : MonoBehaviour
{
    public FloatUnityEvent elapsedTimeUpdateEvent;
    public UnityEvent timeOverEvent;

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
            elapsedTime -= Time.deltaTime;
            elapsedTimeUpdateEvent.Invoke(elapsedTime);
        }
        timeOverEvent.Invoke();
    }
}