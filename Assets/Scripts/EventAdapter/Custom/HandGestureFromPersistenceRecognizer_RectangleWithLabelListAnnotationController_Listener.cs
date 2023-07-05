

using Mediapipe.Unity;
using Mediapipe.Unity.HandTracking;
using System.Collections.Generic;

public class HandGestureFromPersistenceRecognizer_RectangleWithLabelListAnnotationController_Listener :
    EventAdapter<HandGestureFromPersistenceRecognizer, RectangleWithLabelListAnnotationController>
{

    public override void Listen()
    {
        this.register.handGestureRecognizedEvent.AddListener(this.OnGestureOutput);
    }

    private void OnGestureOutput(List<HandGestureRecognizeData> recognizeDatas)
    {
        List<string> labels = new List<string>();
        foreach (var data in recognizeDatas)
        {
            string label = (data.recognizedSample != null ? data.recognizedSample.gestureName : "???");
            labels.Add(label + ":" + data.score.ToString());
        }
        this.listener.DrawLater(labels);
    }
}