using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Towel : MonoBehaviour
{
    private bool isBeingPicked = false;

    private void Start()
    {
        HandGestureInputSystem.ListenToActionKeepEvent(HandGestureAction.PICK, this.Pick);
        HandGestureInputSystem.ListenToActionStopEvent(HandGestureAction.PICK, this.Unpick);
    }

    private void Pick(HandGestureInputEventArgs eventArgs) {
        isBeingPicked = true;
    }
    private void Unpick(HandGestureInputEventArgs eventArgs) { isBeingPicked = false; }

    private void OnTriggerEnter(Collider other)
    {
        if (isBeingPicked) {
            Debug.Log("This towel is being picked!");
        }
    }
}
