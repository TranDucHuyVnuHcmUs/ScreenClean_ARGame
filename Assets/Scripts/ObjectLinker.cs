
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Link many objects with this object. If this object get destroyed then all the linked objects will be destroyed also.
/// </summary>
public class ObjectLinker : MonoBehaviour
{
    public List<GameObject> objects;

    private void OnDestroy()
    {
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }
}