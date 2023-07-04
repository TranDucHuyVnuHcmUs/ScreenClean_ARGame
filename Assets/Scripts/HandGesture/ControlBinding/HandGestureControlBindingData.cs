using System;
using System.Collections.Generic;

[System.Serializable]
public class HandGestureControlBind
{
    public string key;
    public string gesturePath;
    [NonSerialized] public HandGestureSample gesture;
}

/// <summary>
/// Store the data of binding a control key to a hand gesture in order to interact.
/// </summary>
[System.Serializable]
public class HandGestureControlBindingData
{
    public List<HandGestureControlBind> bindings;
}