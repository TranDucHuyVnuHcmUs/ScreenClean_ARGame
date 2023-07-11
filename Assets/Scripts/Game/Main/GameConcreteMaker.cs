using System;
using UnityEngine;


public abstract class GameConcreteMaker<T> : GameAgentMaker<T>
    where T : GameConcreteData
{
    public PlaygroundData playgroundData;

    protected override void MakeObjectFromData(T agentData)
    {
        var newObj = Instantiate(prefab);
        this.createdObjects.Add(newObj);
        newObj.transform.parent = parentTransform;
    
        newObj.transform.localPosition = PositionAccordingToPlayRect(agentData.initLocalPosition, playgroundData.screenRectSize);
        newObj.transform.localRotation = Quaternion.Euler(agentData.initLocalRotation);
        newObj.transform.localScale = agentData.initLocalScale;
        InitObject(newObj, agentData); 
    }

    private Vector3 PositionAccordingToPlayRect(Vector3 initLocalPosition, Vector2 screenRectSize)
    {
        return new Vector3(
            initLocalPosition.x * screenRectSize.x, 
            initLocalPosition.y * screenRectSize.y, 
            initLocalPosition.z);
    }
}