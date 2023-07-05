using System;
using System.Collections.Generic;

[System.Serializable]
public class HandGestureControlBind
{
    public string key;
    public string leftGesturePath, rightGesturePath;
    [NonSerialized] public HandGesture leftGesture, rightGesture;

    public HandGestureControlBind(string key, string leftGesturePath, string rightGesturePath)
    {
        this.key = key;
        this.leftGesturePath = leftGesturePath;
        this.rightGesturePath = rightGesturePath;
    }
}

/// <summary>
/// Store the data of binding a control key to a hand gesture in order to interact.
/// </summary>
[System.Serializable]
public class HandGestureControlBindingData
{
    public List<HandGestureControlBind> bindings;

    public HandGestureControlBindingData()
    {
        bindings = new List<HandGestureControlBind>();
    }


}