using UnityEngine;

public class GameAgentData : ScriptableObject
{
}

public class GameConcreteData : GameAgentData
{
    public Vector3 initLocalPosition;
    public Vector3 initLocalRotation;       // in euler angles
    public Vector3 initLocalScale = new Vector3(1,1,1);
}