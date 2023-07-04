using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;


[System.Serializable]
public class HandGestureLandmarkList
{
    public List<Vector3> landmarks;

    public HandGestureLandmarkList()
    {
        landmarks = new List<Vector3>();
    }

    public HandGestureLandmarkList(List<Vector3> landmarks)
    {
        this.landmarks = landmarks;
    }
}


[System.Serializable]
public class HandGestureSample
{
    public string gestureName;
    public string recordDate;
    public List<HandGestureLandmarkList> landmarkLists;

    public HandGestureSample()
    {
        landmarkLists = new List<HandGestureLandmarkList>();
        recordDate = DateTime.Now.ToString("ddMMyyyy_HHmm");
    }
}

[CreateAssetMenu(menuName = "Config/Hand gesture recorder")]
public class HandGesturePersistentStorageObj : ScriptableObject
{
    public string folderPath;       // append with persistent datapath on the beginning of the path.

    private void CreateFolder()
    {
        string fpath = Path.Combine(Application.persistentDataPath, folderPath);
        if (!Directory.Exists(fpath)) {
            Directory.CreateDirectory(fpath);
        }
    }

    public void SaveToPersistence(string filename, string content)
    {
        CreateFolder();
        string path = Path.Combine(Application.persistentDataPath, folderPath, filename);
        using (StreamWriter  sw = new StreamWriter(path))
        {
            sw.WriteLine(content);
            sw.Close();
            Debug.Log("A new hand gesture saved at: " + path);
        }
    }

    public string ReadFromPersistence(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, folderPath, filename);
        using (StreamReader sr = new StreamReader(path))
        {
            return sr.ReadToEnd();
        }
    }
}