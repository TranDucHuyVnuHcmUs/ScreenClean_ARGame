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
    public bool isLeft;                 // left or right hand?
    public string recordDate;
    public List<HandGestureLandmarkList> landmarkLists;

    public HandGestureSample()
    {
        landmarkLists = new List<HandGestureLandmarkList>();
        recordDate = DateTime.Now.ToString("ddMMyyyy_HHmm");
    }

    public HandGestureSample(string gestureName, bool isLeft)
    {
        this.gestureName = gestureName;
        this.isLeft = isLeft;
        landmarkLists = new List<HandGestureLandmarkList>();
        recordDate = DateTime.Now.ToString("ddMMyyyy_HHmm");
    }
}

[CreateAssetMenu(menuName = "Persistence/Hand gesture")]
public class HandGesturePersistentStorageObj : ScriptableObject
{
    public string folderPath;       // append with persistent datapath on the beginning of the path.
    public List<HandGestureSample> leftSamples, rightSamples;
    private bool isInitialized = false;

    private const string LEFT_HAND_FOLDER = "Left";
    private const string RIGHT_HAND_FOLDER = "Right";

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, folderPath);
    }

    private string GetPath(bool isLeft)
    {
        var fpath = GetPath();
        return Path.Combine(fpath, (isLeft ? LEFT_HAND_FOLDER : RIGHT_HAND_FOLDER));
    }

    private void CreateFolder()
    {
        string fpath = Path.Combine(Application.persistentDataPath, folderPath);
        if (!Directory.Exists(fpath)) {
            Directory.CreateDirectory(fpath);
        }
        if (!Directory.Exists(Path.Combine(fpath, LEFT_HAND_FOLDER))) {
            Directory.CreateDirectory(Path.Combine(fpath, LEFT_HAND_FOLDER));
        }
        if (!Directory.Exists(Path.Combine(fpath, RIGHT_HAND_FOLDER))){
            Directory.CreateDirectory(Path.Combine(fpath, RIGHT_HAND_FOLDER));
        }
    }

    public void SaveToPersistence(string filename, HandGestureSample content)
    {
        CreateFolder();
        string path = Path.Combine( GetPath(content.isLeft), filename );
        using (StreamWriter  sw = new StreamWriter(path))
        {
            sw.WriteLine(JsonUtility.ToJson(content));
            sw.Close();
            Debug.Log("A new hand gesture saved at: " + path);
        }
    }

    public HandGestureSample ReadFromPersistence(string filename, bool isLeft)
    {
        string path = Path.Combine(GetPath(isLeft), filename);
        using (StreamReader sr = new StreamReader(path))
            return JsonUtility.FromJson<HandGestureSample>(sr.ReadToEnd());
    }
    public HandGestureSample ReadFromPersistence(string absPath)
    {
        using (StreamReader sr = new StreamReader(absPath))
            return JsonUtility.FromJson<HandGestureSample>(sr.ReadToEnd());
    }

    public List<string> GetSamplePaths(bool isLeft)
    {
        return Directory.GetFiles(GetPath(isLeft)).ToList();
    }
    public List<string> GetAllSamplePaths()
    {
        var lefts = GetSamplePaths(true);
        var rights = GetSamplePaths(false);
        return lefts.Concat(rights).ToList();
    }

    public List<HandGestureSample> ReadSamples(bool isLeft)
    {
        if (isInitialized)
        {
            if (isLeft)
                return this.leftSamples;
            else return this.rightSamples;
        }
        var filePaths = Directory.GetFiles(GetPath(isLeft));
        List<HandGestureSample> samples = new List<HandGestureSample>();
        for (int i = 0; i < filePaths.Length; i++) {
            samples.Add(ReadFromPersistence(filePaths[i]));
        }
        Debug.Log(samples.ToArray().ToString());
        return samples;
    }

    public List<HandGestureSample> ReadAllSamples()
    {
        var lefts = ReadSamples(true);
        var rights = ReadSamples(false);
        return lefts.Concat(rights).ToList();
    }

    internal void Initialize()
    {
        if (isInitialized) return;
        this.leftSamples = ReadSamples(true);
        this.rightSamples = ReadSamples(false);
    }
}