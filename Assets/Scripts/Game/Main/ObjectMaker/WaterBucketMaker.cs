using UnityEngine;

internal class WaterBucketMaker : GameConcreteMaker<WaterBucketData>
{
    internal override void InitObject(GameObject newObj, WaterBucketData agentData)
    {
        newObj.GetComponentInChildren<WaterBucket>().capacity = agentData.capacity;
    }
}