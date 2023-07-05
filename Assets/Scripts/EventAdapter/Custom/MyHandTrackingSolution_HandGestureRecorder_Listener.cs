
using Mediapipe.Unity.HandTracking;
using System;

public class MyHandTrackingSolution_HandGestureRecorder_Listener :
    EventAdapter<MyHandTrackingSolution, HandGestureRecorder>
{
    public override void Listen()
    {
        this.register.OnHandLandmarksOutputEvent.AddListener(this.listener.SetLandmarks);
        this.register.OnHandednessOutputEvent.AddListener(this.listener.SetHandednessFromList);
        //handGestureRecorder?.SetHandedness(eventArgs.value[0].Classification);
    }
}