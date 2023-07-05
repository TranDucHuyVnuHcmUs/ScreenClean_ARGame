using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandGestureInputEventArgs
{
    public string action;
}

public class HandGestureInputEvent : UnityEvent<HandGestureInputEventArgs> { }

/// <summary>
/// This class implement game controls by using hand gesture inputs (through recognizer and recorder)
/// </summary>
public class HandGestureInputSystem : MonoBehaviour
{
    public static HandGestureInputSystem instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else throw new System.Exception("There's already a hand gesture input system!");
    }

    [Header("Data sources")]
    public HandGestureInputSystemConfig configObj;

    public HandGestureRecognizer recognizer;

    [Header("Events")]
    public HandGestureInputEvent controlStartEvent, controlKeepEvent, controlStopEvent;

    private List<string> inUseControls;         // because we have 2 hands, so we can perform 2 hand gesture input at the same time!

    private void Start()
    {
        configObj.Load();
    }

    private void Update()
    {
        
    }
}
