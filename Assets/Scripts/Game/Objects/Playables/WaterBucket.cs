
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public int capacity = 10;
    public WaterBucketUI ui;

    private void Start()
    {
        ui.ShowCapacity(capacity);
    }

    public void GiveWater(int waterAmount)
    {
        capacity -= waterAmount;
        ui.ShowCapacity(capacity);
    }
}