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

    private Dictionary<string, int> nameToIndexControlDict;
    
    public UnityEvent dataInitilizedEvent;


    private void Awake()
    {
        nameToIndexControlDict = new Dictionary<string, int>();
    }

    private void Start()
    {
        Initialize();
    }

    public List<string> GetControls() {
        return handGestureInputSystemConfig.controlNames;
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

        var controls = GetControls();
        for (int i = 0; i < controls.Count; i++) { 
            nameToIndexControlDict[controls[i]] = i; 
        }

        dataInitilizedEvent.Invoke();
    }    

    public void ChangeBinding(string control, int index, bool isLeft)
    {
        int cid = this.nameToIndexControlDict[control];
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
}
