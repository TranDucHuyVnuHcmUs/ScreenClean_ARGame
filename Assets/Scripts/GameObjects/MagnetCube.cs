using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagnetCube : MonoBehaviour
{
    public Transform magnetSlot;
    private Transform draggedTranform;
    private Transform oldParent;
    //public bool toParent = false;       // ask the collider to be this object's sibling (meaning, to be this obeject's parent's child) instead.

    [SerializeField] private TriggerCollisionEventHandler triggerEvents;
    public ColliderUnityEvent objectAttractedEvent;
    public UnityEvent objectLeftEvent;

    private void Awake()
    {
        objectAttractedEvent = new ColliderUnityEvent();
    }

    private void Start()
    {
        triggerEvents.triggerEnterEvent.AddListener(StartAttracting);
        triggerEvents.triggerExitVoidEvent.AddListener(StopAttracting);
    }

    private void StartAttracting(Collider other)
    {
        if (!other.GetComponent<Magnetic>()) return;
        this.oldParent = other.transform.parent;
        other.transform.parent = this.magnetSlot;

        this.draggedTranform = other.transform;
        objectAttractedEvent.Invoke(other);
    }

    private void StopAttracting()
    {
        if (!this.draggedTranform) return;
        this.draggedTranform.parent = oldParent;
        this.draggedTranform = null;
        objectLeftEvent.Invoke();
    }

    private void OnDisable()
    {
        StopAttracting();
    }
}
