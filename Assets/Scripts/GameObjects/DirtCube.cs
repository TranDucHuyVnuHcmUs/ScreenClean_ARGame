using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCube : MonoBehaviour
{
    public int score = 10;

    public void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.isCleaning)
        {
            if (!other.GetComponent<Towel>()) return;
            GameManager.Instance.AddScore(this.score);
            GameManager.Instance.RemoveDirt();
            Destroy(this.gameObject);
        } else
        {
            GameManager.Instance.GameOver();
        }
    }

}
