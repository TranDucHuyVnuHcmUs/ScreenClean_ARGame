using Mediapipe.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetHand : MonoBehaviour
{
    public ObjectsListAnnotationController magnetCubeAnnotationController;
    public HandGestureAction activateAction;

    internal void ActivateBoxAnnotations(List<HandGestureAction> recognizedActions)
    {
        List<bool> bools = new List<bool>();
        for (int i = 0; i < recognizedActions.Count; i++) {
            bools.Add((recognizedActions[i] == this.activateAction));
        }
        this.magnetCubeAnnotationController.SetBools(bools);
    }
}
