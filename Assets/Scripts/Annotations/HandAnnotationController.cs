using Mediapipe.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnnotationController : MonoBehaviour
{
    public MultiHandLandmarkListAnnotation annotation;
    public BoxListAnnotationController boxAnnotationController;
    public Color startCleaningColor;

    public void StartCleaning()
    {
        //boxAnnotationObj.SetActive(true);
        //boxAnnotationController.Activate();
        annotation.SetLeftLandmarkColor(this.startCleaningColor);
        annotation.SetRightLandmarkColor(this.startCleaningColor);
    }
}
