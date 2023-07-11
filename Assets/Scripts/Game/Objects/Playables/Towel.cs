using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Towel : MonoBehaviour
{
    public int capacity = 3;
    public int waterHold = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DirtCube>())
        {
            InteractWithDirt(other.GetComponent<DirtCube>());
        }
        else if (other.GetComponent<WaterBucket>())
        {
            InteractWithWaterBucket(other.GetComponent<WaterBucket>());
        }
    }

    private void InteractWithWaterBucket(WaterBucket waterBucket)
    {
        var waterHoldIncrease = Math.Min(waterBucket.capacity, capacity - waterHold);
        waterBucket.capacity -= waterHoldIncrease;
        waterHold += waterHoldIncrease;
    }

    private void InteractWithDirt(DirtCube dirtCube)
    {
        if (dirtCube.capacity > this.waterHold)
            GamePlay.GameOver();
        else
        {
            this.waterHold -= dirtCube.capacity;
            Destroy(dirtCube.gameObject);
        }
    }
}