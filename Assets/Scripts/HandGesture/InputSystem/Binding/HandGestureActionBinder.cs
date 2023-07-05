using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class HandGestureActionBinder : MonoBehaviour
{
    public HandGestureInputSystemConfig handGestureInputSystemConfig;
    public HandGesturePersistenceObject gesturePersistenceObj;

    private Dictionary<HandGestureAction, int> actionToIndexDict;
    
    public UnityEvent dataInitilizedEvent;


    private void Awake()
    {
        actionToIndexDict = new Dictionary<HandGestureAction, int>();
    }

    private void Start()
    {
        Initialize();
    }

    public List<HandGestureAction> GetActions() {
        return handGestureInputSystemConfig.actions;
    }

    public List<string> GetHandGesturePaths(bool isLeft) {
        return gesturePersistenceObj.GetSamplePaths(isLeft);
    }

    public List<HandGesture> GetHandGestures(bool isLeft){
         return gesturePersistenceObj.ReadSamples(isLeft);
    }

    public HandGestureInputScheme GetBindingData()
    {
        return handGestureInputSystemConfig.scheme;
    }

    public void Initialize()
    {
        handGestureInputSystemConfig.Load();

        var controls = GetActions();
        for (int i = 0; i < controls.Count; i++) { 
            actionToIndexDict[ controls[i]] = i; 
        }

        dataInitilizedEvent.Invoke();
    }    

    public void ChangeBinding(HandGestureAction action, int index, bool isLeft)
    {
        int cid = this.actionToIndexDict[action];
        string p = (index >= 0) ? this.GetHandGesturePaths(isLeft)[index] : null;
        if (isLeft)
        {
            handGestureInputSystemConfig.scheme.bindings[cid].leftGesturePath = p;
        } else
        {
            handGestureInputSystemConfig.scheme.bindings[cid].rightGesturePath = p;
        }
    }

    public void SaveBinding()
    {
        this.handGestureInputSystemConfig.Save();
    }

    internal void ChangeBinding(object control, int v, bool isLeft)
    {
        throw new NotImplementedException();
    }
}
