using System;
using System.Collections.Generic;

[System.Serializable]
public class HandGestureControlBind
{
    public string key;
    public string gesturePath;
    [NonSerialized] public HandGestureSample gesture;

    public HandGestureControlBind(string key, string gesturePath)
    {
        this.key = key;
        this.gesturePath = gesturePath;
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