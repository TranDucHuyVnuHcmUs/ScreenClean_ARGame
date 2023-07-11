using System;
using UnityEngine;
using UnityEngine.UI;

public class TowelUI : MonoBehaviour
{
    public Text capacityLabel;
    public Text waterHoldLabel;

    internal void ShowCapacity(int capacity)
    {
        capacityLabel.text = capacity.ToString();
    }

    internal void ShowWaterHold(int waterHold)
    {
        waterHoldLabel.text = waterHold.ToString();
    }


}