using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum HandGestureAction
{
    UNKNOWN,
    PICK,
    RELEASE,
    LIKE
}

public class HandGestureInputEventArgs
{
    public HandGestureAction action;

    public HandGestureInputEventArgs(HandGestureAction action)
    {
        this.action = action;
    }
}

public class HandGestureInputEvent : UnityEvent<HandGestureInputEventArgs> { }

public class HandGestureActionEvents
{
    public HandGestureInputEvent startEvent;
    public HandGestureInputEvent keepEvent;
    public HandGestureInputEvent stopEvent;

    public HandGestureActionEvents(HandGestureInputEvent startEvent, 
        HandGestureInputEvent keepEvent, 
        HandGestureInputEvent stopEvent)
    {
        this.startEvent = startEvent;
        this.keepEvent = keepEvent;
        this.stopEvent = stopEvent;
    }

    public HandGestureActionEvents()
    {
        startEvent = new HandGestureInputEvent();
        keepEvent = new HandGestureInputEvent();
        stopEvent = new HandGestureInputEvent();
    }
}


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

    [Header("Events")]
    private Dictionary<HandGestureAction, HandGestureActionEvents> actionEvents;

    private Dictionary<HandGestureAction, bool> actionInUseStatus;         // because we have 2 hands, so we can perform 2 hand gesture input at the same time!

    private void Start()
    {
        configObj.Load();
        actionEvents = new Dictionary<HandGestureAction, HandGestureActionEvents>();
        actionInUseStatus = new Dictionary<HandGestureAction, bool>();
        InitInputEvents();
    }

    private void InitInputEvents()
    {
        var actions = configObj.scheme.GetActions();
        for (int i = 0; i < actions.Count; i++) {
            actionEvents[actions[i]] = new HandGestureActionEvents();
            actionInUseStatus[actions[i]] = false;
        }
    }

    private void Update()
    {
        var actions = configObj.scheme.GetActions();
        for (int i = 0; i < actions.Count; i++)
        {
            if (actionInUseStatus[actions[i]])
                TriggerKeepEvent(actions[i]);            // update every frame!
        }
    }

    private void TriggerKeepEvent(HandGestureAction action)
    {
        actionEvents[action].keepEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action kept: " + action.ToString());
    }

    public static void ListenToActionStartEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[action].startEvent.AddListener(method);
    }
    public static void ListenToActionKeepEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[action].keepEvent.AddListener(method);
    }
    public static void ListenToActionStopEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[action].stopEvent.AddListener(method);
    }

    public static void TriggerStartEvent(HandGestureAction action)
    {
        if (action == HandGestureAction.UNKNOWN) return;
        if (!instance.actionEvents.ContainsKey(action)) return;
        instance.actionInUseStatus[action] = true;
        instance.actionEvents[action].startEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action started: " + action.ToString());
    }
    public static void TriggerStopEvent(HandGestureAction action)
    {
        if (action == HandGestureAction.UNKNOWN) return;
        if (!instance.actionEvents.ContainsKey(action)) return;
        instance.actionInUseStatus[action] = false;
        instance.actionEvents[action].stopEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action stopped: " + action.ToString());
    }

    public static void TriggerManyStartEvent(List<HandGestureAction> actions)
    {
        foreach (var action in actions)
            TriggerStartEvent(action);
    }
    public static void TriggerManyStopEvent(List<HandGestureAction> actions)
    {
        foreach (var action in actions)
            TriggerStopEvent(action);
    }

    public static List<HandGestureAction> GetActions() { return instance.configObj.actions; }

    internal static List<HandGestureActionBinding> GetAllBindings()
    {
        return instance.configObj.scheme.bindings;
    }
}
