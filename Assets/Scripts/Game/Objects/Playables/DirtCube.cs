using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCube : GameAgent
{
    public int capacity = 10;
    public DirtCubeUI dirtCubeUI;

    private void Start()
    {
        GamePlay.AddWork(capacity);
        dirtCubeUI.ShowCapacity(capacity);
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
        GamePlay.Work(capacity);
    }

}
