

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class MyHandTrackingSolution_RectangleWithLabelListAnnotationController_Listener :
    EventAdapter<MyHandTrackingSolution, RectangleWithLabelListAnnotationController>
{
    public HandGestureFromPersistenceRecognizer handGestureFromPersistenceRecognizer;

    public override void Listen()
    {
        this.register.OnHandLandmarksRectsOutputEvent.AddListener(this.listener.DrawLater);
        this.register.OnStartRunEvent.AddListener(this.OnStartRun);
    }


    private void OnStartRun()
    {
        var imageSource = ImageSourceProvider.ImageSource;
        this.register.SetupAnnotationControllerPublic(this.listener, imageSource, true);
    }

    private void OnAllOutputSync(MyHandTrackingSolutionOutput output)
    {
        List<string> labels = new List<string>();
        if (handGestureFromPersistenceRecognizer)
        {
            var recognizeData = handGestureFromPersistenceRecognizer.RecognizeGestureReturn(output.handLandmarks, output.handedness);
            foreach (var data in recognizeData)
            {
                string label = (data.recognizedSample != null ? data.recognizedSample.gestureName : "???");
                labels.Add(label + ":" + data.score.ToString());
            }
        }
        else labels = new List<string>(output.handLandmarksRects.Count);

        this.listener.DrawNow(output.handLandmarksRects, labels);
    }
}