using Mediapipe.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCube : GameAgent
{
    public int capacity = 10;
    public DirtCubeUI ui;

    private void Start()
    {
        GamePlay.AddWork(capacity);
        ui.ShowCapacity(capacity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GameHandLandmark>())
        {
            var isClean = other.GetComponent<GameHandLandmark>().isClean;
            if (!isClean) 
                 GamePlay.GameOver();
        }
    }

    private void OnDestroy()
    {
        GamePlay.DecreaseWork(capacity);
    }

    internal void BeCleaned(int waterHold)
    {
        this.capacity -= waterHold;
        this.ui.ShowCapacity(capacity);
        GamePlay.DecreaseWork(waterHold);
    }
}
