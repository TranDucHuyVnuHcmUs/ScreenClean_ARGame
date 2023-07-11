using UnityEngine;

internal class TowelMaker : GameConcreteMaker<TowelData>
{
    internal override void InitObject(GameObject newObj, TowelData agentData)
    {
        newObj.GetComponent<Towel>().capacity = agentData.capacity;
    }
}