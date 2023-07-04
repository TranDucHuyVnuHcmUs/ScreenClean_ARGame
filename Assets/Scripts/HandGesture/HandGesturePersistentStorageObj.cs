using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Linq;

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
    public List<HandGestureSample> samples;
    private bool isInitialized = false;

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, folderPath);
    }

    private void CreateFolder()
    {
        string fpath = Path.Combine(Application.persistentDataPath, folderPath);
        if (!Directory.Exists(fpath)) {
            Directory.CreateDirectory(fpath);
        }
    }

    public void SaveToPersistence(string filename, HandGestureSample content)
    {
        CreateFolder();
        string path = Path.Combine(Application.persistentDataPath, folderPath, filename);
        using (StreamWriter  sw = new StreamWriter(path))
        {
            sw.WriteLine(JsonUtility.ToJson(content));
            sw.Close();
            Debug.Log("A new hand gesture saved at: " + path);
        }
    }

    public HandGestureSample ReadFromPersistence(string filename)
    {
        string path = Path.Combine(Application.persistentDataPath, folderPath, filename);
        using (StreamReader sr = new StreamReader(path))
        {
            return JsonUtility.FromJson<HandGestureSample>(sr.ReadToEnd());
        }
    }
    private HandGestureSample ReadFromPersistence(string path, bool absolutePath)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            return JsonUtility.FromJson<HandGestureSample>(sr.ReadToEnd());
        }
    }

    public List<string> GetAllSamplePaths()
    {
        return Directory.GetFiles(GetPath()).ToList();
    }

    public List<HandGestureSample> ReadAllSample()
    {
        if (isInitialized) return this.samples;

        var filePaths = Directory.GetFiles(GetPath());
        List<HandGestureSample> samples = new List<HandGestureSample>();
        for (int i = 0; i < filePaths.Length; i++) {
            samples.Add(ReadFromPersistence(filePaths[i], true));
        }
        Debug.Log(samples.ToArray().ToString());
        return samples;
    }

    internal void Initialize()
    {
        if (isInitialized) return;
        this.samples = ReadAllSample();
    }
}