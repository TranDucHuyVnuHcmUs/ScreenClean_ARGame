using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Towel : GameAgent
{
    public int capacity = 3;
    public int waterHold = 0;
    public TowelUI ui;

    private void Start()
    {
        ui.ShowCapacity(capacity);
        ui.ShowWaterHold(waterHold);
    }

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
        //waterBucket.capacity -= waterHoldIncrease;
        waterBucket.GiveWater(waterHoldIncrease);
        waterHold += waterHoldIncrease;
        ui.ShowWaterHold(waterHold);
    }

    private void InteractWithDirt(DirtCube dirtCube)
    {
        if (this.waterHold == 0)
            GamePlay.GameOver();
        else if (dirtCube.capacity > this.waterHold)
        {
            dirtCube.BeCleaned((this.waterHold / 2));
            this.waterHold = 0;
            ui.ShowWaterHold(waterHold);
        }
        else
        {
            this.waterHold -= dirtCube.capacity;
            ui.ShowWaterHold(waterHold);
            Destroy(dirtCube.gameObject);
        }
    }
}
