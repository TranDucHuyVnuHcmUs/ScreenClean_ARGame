using UnityEngine;

internal class TowelMaker : GameConcreteMaker<TowelData>
{
    internal override void InitObject(GameObject newObj, TowelData agentData)
    {
        newObj.GetComponentInChildren<Towel>().capacity = agentData.capacity;
    }
}