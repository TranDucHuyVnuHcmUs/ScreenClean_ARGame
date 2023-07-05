using System;
using System.Collections.Generic;

[System.Serializable]
public class HandGestureActionBinding
{
    public HandGestureAction action;
    public string leftGesturePath, rightGesturePath;
    [NonSerialized] public HandGesture leftGesture, rightGesture;

    public HandGestureActionBinding(HandGestureAction action, string leftGesturePath, string rightGesturePath)
    {
        this.action = action;
        this.leftGesturePath = leftGesturePath;
        this.rightGesturePath = rightGesturePath;
    }
}

/// <summary>
/// Store the data of binding a control key to a hand gesture in order to interact.
/// </summary>
[System.Serializable]
public class HandGestureInputScheme
{
    public List<HandGestureActionBinding> bindings;

    public HandGestureInputScheme()
    {
        bindings = new List<HandGestureActionBinding>();
    }

    public List<HandGestureAction> GetActions()
    {
        var actions = new List<HandGestureAction>();
        foreach (var binding in bindings) { actions.Add(binding.action); }
        return actions;
    }
}