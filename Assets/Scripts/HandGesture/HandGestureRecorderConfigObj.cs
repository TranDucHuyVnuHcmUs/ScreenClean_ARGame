using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Hand gesture recorder")]
public class HandGestureRecorderConfigObj : ScriptableObject
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