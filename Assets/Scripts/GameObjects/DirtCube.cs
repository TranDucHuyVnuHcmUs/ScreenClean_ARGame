using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCube : MonoBehaviour
{
    public int score = 10;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Towel>())
        {
            GameManager.Instance.AddScore(this.score);
            GameManager.Instance.RemoveDirt();
            Destroy(this.gameObject);
        }
        else if (!other.GetComponent<GameHandLandmark>())
        {
            var isClean = !other.GetComponent<GameHandLandmark>();
            if (!isClean) 
                 GameManager.Instance.GameOver();
        }
    }

}
