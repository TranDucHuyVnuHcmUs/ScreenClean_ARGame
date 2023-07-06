using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

public class HandGestureActionListUnityEvent: UnityEvent<List<HandGestureAction>> { }
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


    [Header("Data sources")]
    public HandGestureInputSystemConfig configObj;

    private HandGestureActionListUnityEvent actionsRecognizedEvent;

    private List<HandGestureAction> actions;
    private Dictionary<HandGestureAction, int> actionIndexes;
    private List<HandGestureActionEvents> actionEvents;
    private bool[] actionStatuses;          // is an action being used or not?

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else throw new System.Exception("There's already a hand gesture input system!");

        actionsRecognizedEvent = new HandGestureActionListUnityEvent();
        actionEvents = new List<HandGestureActionEvents>();
        actionIndexes = new Dictionary<HandGestureAction, int>();
    }

    private void Start()
    {
        configObj.Load();
        InitInputActionData();
    }

    private void InitInputActionData()
    {
        actions = configObj.scheme.GetActions();

        actionStatuses = new bool[actions.Count];
        for (int i = 0; i < actions.Count; i++) {
            actionIndexes[actions[i]] = i;
            actionEvents.Add(new HandGestureActionEvents());
        }
    }

    private void Update()
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actionStatuses[i])
                TriggerKeepEvent(actions[i]);            // update every frame!
        }
    }

    private void TriggerKeepEvent(HandGestureAction action)
    {
        actionEvents[FindIndex(action)].keepEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action kept: " + action.ToString());
    }

    private static int FindIndex(HandGestureAction action) {
        if (action == HandGestureAction.UNKNOWN) return -1;
        return instance.actionIndexes[action];
    }


    #region events

    public static void ListenToActionsRecognizedEvent(UnityAction<List<HandGestureAction>> actionsMethod)
    {
        instance.actionsRecognizedEvent.AddListener(actionsMethod);
    }
    public static void UnregisterToActionsRecognizedEvent(UnityAction<List<HandGestureAction>> actionsMethod)
    {
        instance.actionsRecognizedEvent.RemoveListener(actionsMethod);
    }


    public static void ListenToActionStartEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].startEvent.AddListener(method);
    }
    public static void ListenToActionKeepEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].keepEvent.AddListener(method);
    }
    public static void ListenToActionStopEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].stopEvent.AddListener(method);
    }

    public static void ListenToAction(HandGestureAction action,
        UnityAction<HandGestureInputEventArgs> startActionEventListener,
        UnityAction<HandGestureInputEventArgs> keepActionEventListener,
        UnityAction<HandGestureInputEventArgs> stopActionEventListener)
    {
        if (startActionEventListener != null) ListenToActionStartEvent(action, startActionEventListener);
        if (keepActionEventListener != null) ListenToActionKeepEvent(action, keepActionEventListener);
        if (stopActionEventListener != null) ListenToActionStopEvent(action, stopActionEventListener);
    }

    public static void UnregisterToActionStartEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].startEvent.RemoveListener(method);
    }
    public static void UnregisterToActionKeepEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].keepEvent.RemoveListener(method);
    }
    public static void UnregisterToActionStopEvent(HandGestureAction action, UnityAction<HandGestureInputEventArgs> method)
    {
        instance.actionEvents[FindIndex(action)].stopEvent.RemoveListener(method);
    }
    public static void UnregisterToAction(HandGestureAction action,
        UnityAction<HandGestureInputEventArgs> startActionEventListener,
        UnityAction<HandGestureInputEventArgs> keepActionEventListener,
        UnityAction<HandGestureInputEventArgs> stopActionEventListener)
    {
        if (startActionEventListener != null) UnregisterToActionStartEvent(action, startActionEventListener);
        if (keepActionEventListener != null) UnregisterToActionKeepEvent(action, keepActionEventListener);
        if (stopActionEventListener != null) UnregisterToActionStopEvent(action, stopActionEventListener);
    }


    public static void CallActionStartEvent(HandGestureAction action)
    {
        if (action == HandGestureAction.UNKNOWN) return;
        if (!instance.actionIndexes.ContainsKey(action)) return;

        instance.actionStatuses[FindIndex(action)] = true;
        instance.actionEvents[FindIndex(action)].startEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action started: " + action.ToString());
    }
    public static void CallActionStopEvent(HandGestureAction action)
    {
        if (action == HandGestureAction.UNKNOWN) return;
        if (!instance.actionIndexes.ContainsKey(action)) return;

        instance.actionStatuses[FindIndex(action)] = false;
        instance.actionEvents[FindIndex(action)].stopEvent.Invoke(new HandGestureInputEventArgs(action));
        Debug.Log("Action stopped: " + action.ToString());
    }

    #endregion

    public static void UpdateActionStatuses(List<HandGestureAction> recognizedActions)
    {
        CallActionRecognizedEvent(recognizedActions);
        List<int> recognizedActionIndexes = FindIndexes(recognizedActions);

        bool[] newActionStatuses = new bool[instance.actionStatuses.Length];
        Array.Fill(newActionStatuses, false);

        for (int i = 0; i < recognizedActionIndexes.Count; i++)
        {
            var index = recognizedActionIndexes[i];
            if (index == -1) continue;
            if (!instance.actionStatuses[index]) {
                CallActionStartEvent(recognizedActions[i]);
            }
            newActionStatuses[index] = true;
        }

        for (int i = 0; i < instance.actionStatuses.Length; i++)
        {
            if (instance.actionStatuses[i] && !newActionStatuses[i])        // that means it's turn from on to off
            {
                CallActionStopEvent(instance.actions[i]);
            }
        }
    }

    private static void CallActionRecognizedEvent(List<HandGestureAction> recognizedActions)
    {
        instance.actionsRecognizedEvent.Invoke(recognizedActions);
    }

    private static List<int> FindIndexes(IList<HandGestureAction> recognizedActions)
    {
        List<int> indexes = new List<int>();
        foreach (var action in recognizedActions)
            if (action == HandGestureAction.UNKNOWN) indexes.Add(-1);
            else indexes.Add(FindIndex(action));
        return indexes;
    }

    public static List<HandGestureAction> GetActions() { return instance.configObj.actions; }

    internal static List<HandGestureActionBinding> GetAllBindings()
    {
        return instance.configObj.scheme.bindings;
    }
}
