

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class MyHandTrackingSolution_BoxListAnnotationController_Listener :
    EventAdapter<MyHandTrackingSolution, BoxListAnnotationController>
{
    public HandGestureFromPersistenceRecognizer handGestureFromPersistenceRecognizer;

    public override void Listen()
    {
        this.register.OnHandLandmarksRectsOutputEvent.AddListener(this.listener.DrawLater);
        //this.register.OnStartRunEvent.AddListener(this.OnStartRun);
    }

    //private void OnStartRun()
    //{
    //    var imageSource = ImageSourceProvider.ImageSource;
    //    this.register.SetupAnnotationControllerPublic(this.listener, imageSource, true);
    //}
}