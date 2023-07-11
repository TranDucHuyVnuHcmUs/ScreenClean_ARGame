using System;
using System.Collections.Generic;
using UnityEngine;

public interface GameAgentMaker
{
    public void CleanObjects();
    public void MakeObject(GameAgentData agentData);
}

public abstract class GameAgentMaker<T> : MonoBehaviour, GameAgentMaker
    where T : GameAgentData
{
    protected List<GameObject> createdObjects;
    public GameObject prefab;
    public Transform parentTransform;

    private void Awake()
    {
        createdObjects = new List<GameObject>();
    }

    public virtual void MakeObject(GameAgentData agentData)
    {
        MakeObjectFromData((T)agentData);
    }

    protected virtual void MakeObjectFromData(T agentData)
    {
        var newObj = Instantiate(prefab);
        createdObjects.Add(newObj);

        newObj.transform.parent = parentTransform;
        InitObject(newObj, agentData);
    }

    internal abstract void InitObject(GameObject newObj, T agentData);

    public void CleanObjects()
    {
        while (this.createdObjects.Count > 0)
        {
            var obj = createdObjects[0];
            createdObjects.RemoveAt(0);
            Destroy(obj);
        }
    }
}