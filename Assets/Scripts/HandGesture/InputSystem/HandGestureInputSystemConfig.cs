using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "HandGestureInputSystemConfig",menuName = "Input/Hand gesture/Config")]
public class HandGestureInputSystemConfig : ScriptableObject
{
    public List<string> controlNames;
    public HandGestureInputScheme scheme;
    public HandGestureInputSchemePersistenceObject bindingPersistenceObj;
    
    public void Load()
    {
        this.scheme = bindingPersistenceObj.ReadFromPersistence();
        if (scheme == null)
            this.scheme = bindingPersistenceObj.InitializeNewFile(this.controlNames);
    }

    internal void Save()
    {
        this.bindingPersistenceObj.SaveToPersistence(scheme);
    }
}