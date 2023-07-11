using UnityEngine;

internal class DirtMaker : GameConcreteMaker<DirtData>
{
    internal override void InitObject(GameObject newObj, DirtData agentData)
    {
        newObj.GetComponentInChildren<DirtCube>().capacity = agentData.capacity;
    }
}