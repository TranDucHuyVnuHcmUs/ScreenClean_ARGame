using System;
using UnityEngine;

public class DirtRandomizer : GameConcreteMaker<DirtRandomizerData>
{
    protected override void MakeObjectFromData(DirtRandomizerData agentData)
    {
        for (int i = 0; i < agentData.number; i++)
        {
            var newObj = Instantiate(prefab);
            newObj.transform.parent = parentTransform;
            InitObject(newObj, agentData);

        }
    }

    private Vector3 RandomizeVector(Vector3 minRange, Vector3 maxRange)
    {
        var x = UnityEngine.Random.Range(minRange.x, maxRange.x);
        var y = UnityEngine.Random.Range(minRange.y, maxRange.y);
        var z = UnityEngine.Random.Range(minRange.z, maxRange.z);
        return new Vector3(x, y, z);
    }

    internal override void InitObject(GameObject newObj, DirtRandomizerData agentData)
    {
        newObj.transform.localPosition = RandomizeVector(agentData.minRange, agentData.maxRange);
        newObj.transform.localPosition = PositionAccordingToPlayRect(newObj.transform.localPosition, playgroundData.screenRectSize);
        newObj.transform.localRotation = Quaternion.Euler(agentData.initLocalRotation);
        newObj.transform.localScale = agentData.initLocalScale;
    }

    private Vector3 PositionAccordingToPlayRect(Vector3 localPosition, Vector2 screenRectSize)
    {
        return new Vector3(
            localPosition.x * screenRectSize.x,
            localPosition.y * screenRectSize.y,
            localPosition.z);
    }
}