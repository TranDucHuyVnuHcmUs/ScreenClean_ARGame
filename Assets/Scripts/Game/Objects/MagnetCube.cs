using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MagnetCube : MonoBehaviour
{
    public Transform magnetSlot;
    [SerializeField] private bool isDragging = false;
    private Transform draggedTransform;
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

        this.draggedTransform = other.transform;
        isDragging = true;
        other.GetComponent<Magnetic>().draggingMagnet = this;
        objectAttractedEvent.Invoke(other);
    }

    internal void StopAttracting()
    {
        if (!this.draggedTransform) return;
        this.draggedTransform.parent = oldParent;
        this.draggedTransform = null;
        isDragging = false;
        objectLeftEvent.Invoke();
    }


    private void OnDisable()
    {
        StopAttracting();
    }

    internal void DestroyDraggingObject()
    {
        this.draggedTransform = null;
        isDragging = false;
        objectLeftEvent.Invoke();
    }
}
