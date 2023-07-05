using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "HandGestureInputSystemConfig",menuName = "Input/Hand gesture/Config")]
public class HandGestureInputSystemConfig : ScriptableObject
{
    public List<HandGestureAction> actions;
    public HandGestureInputScheme scheme;
    public HandGestureInputSchemePersistenceObject bindingPersistenceObj;
    
    public void Load()
    {
        this.scheme = bindingPersistenceObj.ReadFromPersistence();
        if (scheme == null)
            this.scheme = bindingPersistenceObj.InitializeNewFile(this.actions);
    }

    internal void Save()
    {
        this.bindingPersistenceObj.SaveToPersistence(scheme);
    }
}