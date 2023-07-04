using Mediapipe;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public class StringUnityEvent : UnityEvent<string> { }

public class HandGestureRecognizeDataListUnityEvent: UnityEvent<List<HandGestureRecognizeData>> { }

public class HandGestureRecognizeData
{
    public HandGestureSample recognizedSample;
    public HandGestureCompareMetric metric;
    public float score;

    public HandGestureRecognizeData(HandGestureSample recognizedSample, HandGestureCompareMetric metric, float score)
    {
        this.recognizedSample = recognizedSample;
        this.metric = metric;
        this.score = score;
    }
}

public class HandGestureRecognizer : MonoBehaviour
{
    public HandGesturePersistentStorageObj handGesturePersistentStorageObj;
    public HandGestureCompareMetric metric;     // Strategy pattern
    public float threshold;

    [Header("Events")]
    public HandGestureRecognizeDataListUnityEvent handGestureRecognizedEvent;

    private void Awake()
    {
        handGestureRecognizedEvent = new HandGestureRecognizeDataListUnityEvent();
    }

    private void Start()
    {
        handGesturePersistentStorageObj.Initialize();           // ask it to fill the data
    }

    internal HandGestureRecognizeData RecognizeLandmarks(NormalizedLandmarkList normalizedLandmarkList)
    {
        float maxScore = -999;
        int index = -1;
        for (int i = 0; i < handGesturePersistentStorageObj.samples.Count; i++) 
        {
            var sample = handGesturePersistentStorageObj.samples[i];
            var score = this.metric.Score(sample, normalizedLandmarkList);
            if (maxScore < score) {
                index = i;
                maxScore = score;
            }
            
        }
        //handGestureRecognizedEvent.Invoke(handGesturePersistentStorageObj.samples[index].gestureName);
        if (maxScore >= threshold)
            return new HandGestureRecognizeData(handGesturePersistentStorageObj.samples[index], this.metric, maxScore);
        else return new HandGestureRecognizeData(null, this.metric, maxScore);
    }

    public List<HandGestureRecognizeData> RecognizeGestureSync(List<NormalizedLandmarkList> landmarksList)
    {
        List<HandGestureRecognizeData> recognizedSamples = new List<HandGestureRecognizeData>();
        foreach (var landmarks in landmarksList)
        {
            recognizedSamples.Add(RecognizeLandmarks(landmarks));
        }
        return recognizedSamples;
    }

    internal void GetLandmarks(List<NormalizedLandmarkList> landmarksList)
    {
        List<HandGestureRecognizeData> recognizedSamples = new List<HandGestureRecognizeData>();
        foreach (var landmarks in landmarksList)
        {
            recognizedSamples.Add(RecognizeLandmarks(landmarks));
        }
        handGestureRecognizedEvent.Invoke(recognizedSamples);
    }
}