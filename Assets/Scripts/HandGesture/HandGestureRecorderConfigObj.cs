using UnityEngine;

[CreateAssetMenu(menuName = "Config/Hand gesture recorder")]
public class HandGestureRecorderConfigObj : ScriptableObject
{
    public string folderPath;       // append with persistent datapath on the beginning of the path.
}