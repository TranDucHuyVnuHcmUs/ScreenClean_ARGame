using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCube : MonoBehaviour
{
    public int capacity = 10;

    private void Start()
    {
        GamePlay.AddWork(capacity);
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
