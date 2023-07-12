using System;
using UnityEngine;
using UnityEngine.UI;

public class DirtCubeUI : MonoBehaviour
{
    public Text capacityLabel;

    public void ShowCapacity(int capacity)
    {
        capacityLabel.text = capacity.ToString();
    }
}