using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Persistence/Hand gesture binding")]
public class HandGestureControlBindingPersistentStorageObj : ScriptableObject
{
    public string fileName = "controlBinding.json";
    public HandGestureControlBindingData bindingData;
    public HandGesturePersistentStorageObj handGesturePersistentStorageObj;

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
                bindingData.bindings[i].gesture = handGesturePersistentStorageObj.ReadFromPersistence(bindingData.bindings[i].gesturePath);
            }
        }
    }
}