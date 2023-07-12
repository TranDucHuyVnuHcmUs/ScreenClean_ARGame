

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class HandGestureInputSystem_GameMultiHandLandmarkListAnnotationController_Listener :
    EventAdapter<HandGestureInputSystem, GameMultiHandLandmarkListAnnotationController>
{
    public override void Listen()
    {
        HandGestureInputSystem.ListenToActionsRecognizedEvent(this.ActivateObjectsAnnotations);
    }

    internal void ActivateObjectsAnnotations(List<HandGestureAction> recognizedActions)
    {
        List<bool> bools = new List<bool>();
        for (int i = 0; i < recognizedActions.Count; i++)
        {
            bools.Add((recognizedActions[i] == this.listener.activateObjectAction)) ;
        }
        this.listener.DrawLaterActiveness(bools);
    }

}