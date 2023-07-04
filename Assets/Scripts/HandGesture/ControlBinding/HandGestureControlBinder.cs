using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestureControlBinder : MonoBehaviour
{
    public HandGestureControlBinderUI ui;
    public HandGestureControlBindingPersistentStorageObj bindingPersistenceObj;     //obj with persistence feature
    public HandGesturePersistentStorageObj gesturePersistenceObj;

    private List<HandGestureSample> handGestureSamples;
    private List<string> handGesturePaths;

    private void Start()
    {
        Initialize();
        InitUI();
    }

    private void InitUI()
    {
        ui.Initialize(this.handGestureSamples);
    }

    public void Initialize()
    {
        this.handGestureSamples = gesturePersistenceObj.ReadAllSample();
        this.handGesturePaths = gesturePersistenceObj.GetAllSamplePaths();
    }    

    public void ChangeBinding()
    {
        
    }



}
