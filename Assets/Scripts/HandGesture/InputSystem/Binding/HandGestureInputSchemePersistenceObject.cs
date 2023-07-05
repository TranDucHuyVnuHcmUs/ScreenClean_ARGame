using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Persistence/Hand gesture input scheme")]
public class HandGestureInputSchemePersistenceObject : ScriptableObject
{
    public string fileName = "controlBinding.json";
    public HandGesturePersistenceObject handGesturePersistentStorageObj;


    public void SaveToPersistence(HandGestureInputScheme newScheme)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        using (StreamWriter writer = new StreamWriter(path, false)) { 
            writer.WriteLine( JsonUtility.ToJson(newScheme) );
        }
    }

    public HandGestureInputScheme ReadFromPersistence()
    {
     //   if (this.isInitialized) return this.bindingData;

        string path = Path.Combine(Application.persistentDataPath, fileName);
        var scheme = new HandGestureInputScheme();
        if (!File.Exists(path)) return null;

        using (StreamReader reader = new StreamReader(path)){
            scheme = JsonUtility.FromJson<HandGestureInputScheme>(reader.ReadToEnd());
            for (int i = 0; i < scheme.bindings.Count; i++) {
                scheme.bindings[i].leftGesture = handGesturePersistentStorageObj.ReadFromPersistence(scheme.bindings[i].leftGesturePath);
                scheme.bindings[i].rightGesture = handGesturePersistentStorageObj.ReadFromPersistence(scheme.bindings[i].rightGesturePath);
            }
        }
        return scheme;
    }

    public HandGestureInputScheme InitializeNewFile(List<HandGestureAction> actions)
    {
        var scheme = new HandGestureInputScheme();
        for (int i = 0; i < actions.Count; i++)
            scheme.bindings.Add(new HandGestureActionBinding(actions[i], null, null));
        SaveToPersistence(scheme);
        return scheme;
    }
}