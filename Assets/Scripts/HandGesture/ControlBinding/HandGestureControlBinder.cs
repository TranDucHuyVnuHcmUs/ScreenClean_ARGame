using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class HandGestureControlBinder : MonoBehaviour
{
    public HandGestureControlBindingPersistentStorageObj bindingPersistenceObj;     //obj with persistence feature
    public HandGesturePersistentStorageObj gesturePersistenceObj;

    public List<HandGestureSample> leftHandGestureSamples, rightHandGestureSamples;
    private List<string> leftHandGesturePaths, rightHandGesturePaths;

    private Dictionary<string, int> nameToIndexControlDict;
    
    public int index = 0;

    public UnityEvent dataInitilizedEvent;


    private void Start()
    {
        Initialize();
        dataInitilizedEvent.Invoke();
    }


    public void Initialize()
    {
        this.leftHandGestureSamples = gesturePersistenceObj.ReadSamples(true);
        this.leftHandGesturePaths = gesturePersistenceObj.GetSamplePaths(true);
        this.rightHandGestureSamples = gesturePersistenceObj.ReadSamples(false);
        this.rightHandGesturePaths = gesturePersistenceObj.GetSamplePaths(false);
        bindingPersistenceObj.ReadFromPersistence();
    }    

    public void ChangeBinding(string control, int index, bool isLeft)
    {
        int cid = this.nameToIndexControlDict[control];
        if (isLeft)
        {
            bindingPersistenceObj.bindingData.bindings[cid].leftGesturePath = this.leftHandGesturePaths[index];
        } else
        {
            bindingPersistenceObj.bindingData.bindings[cid].rightGesturePath = this.rightHandGesturePaths[index];
        }
    }

    public void SaveBinding()
    {
        this.bindingPersistenceObj.SaveToPersistence();
    }    
}
