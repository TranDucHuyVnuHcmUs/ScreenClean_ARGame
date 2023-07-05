using System;
using System.Collections.Generic;

[System.Serializable]
public class HandGestureActionBinding
{
    public string action;
    public string leftGesturePath, rightGesturePath;
    [NonSerialized] public HandGesture leftGesture, rightGesture;

    public HandGestureActionBinding(string action, string leftGesturePath, string rightGesturePath)
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

    public List<string> GetActions()
    {
        var actions = new List<string>();
        foreach (var binding in bindings) { actions.Add(binding.action); }
        return actions;
    }
}