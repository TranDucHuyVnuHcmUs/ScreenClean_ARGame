using Mediapipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

//public class StringUnityEvent : UnityEvent<string> { }

public class HandGestureRecognizeDataListUnityEvent: UnityEvent<List<HandGestureRecognizeData>> { }

public class HandGestureRecognizeData
{
    public HandGesture recognizedSample;
    public HandGestureCompareMetric metric;
    public float score;

    public HandGestureRecognizeData(HandGesture recognizedSample, HandGestureCompareMetric metric, float score)
    {
        this.recognizedSample = recognizedSample;
        this.metric = metric;
        this.score = score;
    }
}

/// <summary>
/// The recognizer use a metric to compare a landmark with the list of samples, and return the best sample with the highest score. You can also use events.
/// </summary>
[CreateAssetMenu(fileName = "HandGestureRecognizer", menuName = "Hand gesture/Recognizer")]
public class HandGestureRecognizer : ScriptableObject
{
    //public HandGesturePersistentStorageObj handGesturePersistentStorageObj;
    public HandGestureOneToManyCompareMetric oneToManyMetric;
    public HandGestureCompareMetric metric;     // Strategy pattern
    public float threshold;

    public HandGestureRecognizeData RecognizeLandmarksFromSamples(
        HandLandmarkList landmarks, 
        List<HandGesture> gestures,
        out int maxScoreIndex)
    {
        int index = -1;
        float maxScore = oneToManyMetric.Score(landmarks, gestures, out index);

        maxScoreIndex = index;
        if (maxScore >= threshold) {
            return new HandGestureRecognizeData(gestures[index], this.metric, maxScore);
        }
        else {
            return new HandGestureRecognizeData(null, this.metric, maxScore); 
        }
    }
}