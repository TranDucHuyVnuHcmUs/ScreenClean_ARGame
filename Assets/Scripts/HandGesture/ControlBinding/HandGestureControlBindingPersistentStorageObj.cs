using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Persistence/Hand gesture binding")]
public class HandGestureControlBindingPersistentStorageObj : ScriptableObject
{
    public string fileName = "controlBinding.json";
    public List<string> controlNames;
    public HandGestureControlBindingData bindingData;
    public HandGesturePersistentStorageObj handGesturePersistentStorageObj;
    public bool isInitialized = false;

    public void SaveToPersistence()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (StreamWriter writer = new StreamWriter(path, false)) { 
            writer.WriteLine( JsonUtility.ToJson(bindingData) );
        }
    }

    public HandGestureControlBindingData ReadFromPersistence()
    {
     //   if (this.isInitialized) return this.bindingData;

        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            InitializeNewFile();
            isInitialized = true;
            return this.bindingData;
        }
        using (StreamReader reader = new StreamReader(path)){
            this.bindingData = JsonUtility.FromJson<HandGestureControlBindingData>(reader.ReadToEnd());
            for (int i = 0; i < this.bindingData.bindings.Count; i++) {
                bindingData.bindings[i].leftGesture = handGesturePersistentStorageObj.ReadFromPersistence(bindingData.bindings[i].leftGesturePath);
                bindingData.bindings[i].rightGesture = handGesturePersistentStorageObj.ReadFromPersistence(bindingData.bindings[i].rightGesturePath);
            }
        }
        isInitialized = true;
        return this.bindingData;
    }

    private void InitializeNewFile()
    {
        this.bindingData = new HandGestureControlBindingData();
        for (int i = 0; i < this.controlNames.Count; i++)
            this.bindingData.bindings.Add(new HandGestureControlBind(this.controlNames[i], null, null));
        SaveToPersistence();
    }
}