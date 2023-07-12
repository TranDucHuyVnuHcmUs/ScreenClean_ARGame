using System;
using UnityEngine;
using UnityEngine.UI;

public class WaterBucketUI : MonoBehaviour
{
    public Text capacityLabel;

    internal void ShowCapacity(int capacity)
    {
        capacityLabel.text = capacity.ToString();
    }
}