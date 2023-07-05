using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCube : MonoBehaviour
{
    private Transform draggedTranform;
    private Transform oldParent;
    public bool toParent = false;       // ask the collider to be this object's sibling (meaning, to be this obeject's parent's child) instead.

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Magnetic>()) return;
        this.oldParent = other.transform.parent;
        if (!toParent )
            other.transform.parent = this.transform;
        else other.transform.parent = this.transform.parent;

        this.draggedTranform = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.draggedTranform) return;
        other.transform.parent = oldParent;
        this.draggedTranform = null;
    }

    private void OnDisable()
    {
        if (!this.draggedTranform ) return;
        this.draggedTranform.parent = oldParent;
        this.draggedTranform = null;
    }
}
