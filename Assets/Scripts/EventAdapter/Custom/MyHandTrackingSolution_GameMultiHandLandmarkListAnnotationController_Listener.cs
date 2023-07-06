

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class MyHandTrackingSolution_GameMultiHandLandmarkListAnnotationController_Listener :
    EventAdapter<MyHandTrackingSolution, GameMultiHandLandmarkListAnnotationController>
{
    public override void Listen()
    {
        this.register.OnHandLandmarksOutputEvent.AddListener(this.listener.DrawLater);
        this.register.OnHandednessOutputEvent.AddListener(this.listener.DrawLater);
        this.register.OnHandLandmarksRectsOutputEvent.AddListener(this.listener.DrawLater);
        this.register.OnStartRunEvent.AddListener(this.OnStartRun);
        this.register.OnAllOutputSyncEvent.AddListener(this.OnAllOutputSyncEvent);
    }

    private void OnStartRun()
    {
        var imageSource = ImageSourceProvider.ImageSource;
        this.register.SetupAnnotationControllerPublic(this.listener, imageSource, true);
    }
    
    private void OnAllOutputSyncEvent(MyHandTrackingSolutionOutput output)
    {
        this.listener.DrawNow(output.handLandmarks, output.handedness);
    }

}