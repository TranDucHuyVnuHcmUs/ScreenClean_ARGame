using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtCubeDemo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Towel>())
            Destroy(this.gameObject);
    }
}
