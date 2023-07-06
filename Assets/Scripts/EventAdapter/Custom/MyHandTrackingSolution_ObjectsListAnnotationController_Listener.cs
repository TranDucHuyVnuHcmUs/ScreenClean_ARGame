

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class MyHandTrackingSolution_ObjectsListAnnotationController_Listener :
    EventAdapter<MyHandTrackingSolution, ObjectsListAnnotationController>
{
    public HandGestureFromPersistenceRecognizer handGestureFromPersistenceRecognizer;

    public override void Listen()
    {
        this.register.OnHandLandmarksRectsOutputEvent.AddListener(this.listener.DrawLater);
    }
}