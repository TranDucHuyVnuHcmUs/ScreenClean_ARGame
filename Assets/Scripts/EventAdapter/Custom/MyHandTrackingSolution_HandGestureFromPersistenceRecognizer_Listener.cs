

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class MyHandTrackingSolution_HandGestureFromPersistenceRecognizer_Listener :
    EventAdapter<MyHandTrackingSolution, HandGestureFromPersistenceRecognizer>
{

    public override void Listen()
    {
        this.register.OnHandLandmarksOutputEvent.AddListener(this.listener.SetNormalizedLandmarkList);
        this.register.OnHandednessOutputEvent.AddListener(this.listener.SetHandednessList);
        //handGestureFromPersistenceRecognizer?.SetHandednessList(eventArgs.value);
    }

}