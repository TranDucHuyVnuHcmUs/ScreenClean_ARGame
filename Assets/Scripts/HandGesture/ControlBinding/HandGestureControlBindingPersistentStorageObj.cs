using System.IO;
using UnityEngine;

public class HandGestureControlBindingPersistentStorageObj : ScriptableObject
{
    public string fileName;
    public HandGestureControlBindingData bindingData;

    private void Awake()
    {
        ReadFromPersistence();
    }

    public void SaveToPersistence()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        using (StreamWriter writer = new StreamWriter(path, false)) { 
            writer.WriteLine( JsonUtility.ToJson(bindingData) );
        }
    }

    public void ReadFromPersistence()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path)) return;
        using (StreamReader reader = new StreamReader(path)){
            this.bindingData = JsonUtility.FromJson<HandGestureControlBindingData>(reader.ReadToEnd());
            for (int i = 0; i < this.bindingData.bindings.Count; i++) {

            }
        }
    }
}