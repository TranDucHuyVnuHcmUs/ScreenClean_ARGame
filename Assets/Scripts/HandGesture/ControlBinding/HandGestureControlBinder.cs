using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class HandGestureControlBinder : MonoBehaviour
{
    public HandGestureControlBindingPersistentStorageObj bindingPersistenceObj;     //obj with persistence feature
    public HandGesturePersistentStorageObj gesturePersistenceObj;

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
        return bindingPersistenceObj.controlNames;
    }

    public List<string> GetHandGesturePaths(bool isLeft) {
        return gesturePersistenceObj.GetSamplePaths(isLeft);
    }

    public List<HandGesture> GetHandGestures(bool isLeft){
         return gesturePersistenceObj.ReadSamples(isLeft);
    }

    public HandGestureControlBindingData GetBindingData()
    {
        return bindingPersistenceObj.ReadFromPersistence();
    }

    public void Initialize()
    {
        bindingPersistenceObj.ReadFromPersistence();

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
            bindingPersistenceObj.bindingData.bindings[cid].leftGesturePath = p;
        } else
        {
            bindingPersistenceObj.bindingData.bindings[cid].rightGesturePath = p;
        }
    }

    public void SaveBinding()
    {
        this.bindingPersistenceObj.SaveToPersistence();
    }    
}
