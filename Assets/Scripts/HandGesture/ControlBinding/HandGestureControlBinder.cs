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
    
    public int index = 0;


    private void Start()
    {
        ui.pickKeyGestureDropdown.onValueChanged.AddListener(ChangeBinding);
        Initialize();
        InitUI();
        ChangeBinding(this.index);            // set the default index
    }

    private void InitUI()
    {
        ui.Initialize(this.handGestureSamples);
    }

    public void Initialize()
    {
        this.handGestureSamples = gesturePersistenceObj.ReadAllSample();
        this.handGesturePaths = gesturePersistenceObj.GetAllSamplePaths();
        bindingPersistenceObj.ReadFromPersistence();
    }    

    public void ChangeBinding(int index)
    {
        bindingPersistenceObj.bindingData.bindings[0].gesturePath = this.handGesturePaths[index];
    }

    public void SaveBinding()
    {
        this.bindingPersistenceObj.SaveToPersistence();
    }    
}
