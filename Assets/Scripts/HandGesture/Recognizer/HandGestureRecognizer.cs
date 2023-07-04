using Mediapipe;
using System;
using System.Collections.Generic;
using System.Linq;
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

    private List<NormalizedLandmarkList> currentNormalizedLandmarkLists;
    private List<ClassificationList> handednessLists;

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

    internal HandGestureRecognizeData RecognizeLandmarks(NormalizedLandmarkList normalizedLandmarkList, 
        ClassificationList handednessList)
    {
        float maxScore = -999;
        int index = -1;

        bool isLeft = IsHandLeft(handednessList.Classification);

        List<HandGestureSample> samples = (isLeft) ? handGesturePersistentStorageObj.leftSamples : handGesturePersistentStorageObj.rightSamples;
        for (int i = 0; i < samples.Count; i++)
        {
            var sample = samples[i];
            var score = this.metric.Score(sample, normalizedLandmarkList);
            if (maxScore < score)
            {
                index = i;
                maxScore = score;
            }
        }

        //handGestureRecognizedEvent.Invoke(handGesturePersistentStorageObj.samples[index].gestureName);
        if (maxScore >= threshold)
            return new HandGestureRecognizeData(samples[index], this.metric, maxScore);
        else return new HandGestureRecognizeData(null, this.metric, maxScore);
    }

    private bool IsHandLeft(IList<Classification> handedness)
    {
        if (handedness == null || handedness.Count == 0 || handedness[0].Label == "Left")
        {
            return true;
        }
        else if (handedness[0].Label == "Right")
        {
            return false;
        }
        else return false;
    }

    public List<HandGestureRecognizeData> RecognizeGestureReturn(List<NormalizedLandmarkList> landmarksLists,
        List<ClassificationList> handednessLists)
    {
        List<HandGestureRecognizeData> recognizedSamples = new List<HandGestureRecognizeData>();
        for (int i = 0; i < landmarksLists.Count; i++)
        {
            recognizedSamples.Add(RecognizeLandmarks(landmarksLists[i], handednessLists[i]));
        }
        return recognizedSamples;
    }

    public void SetNormalizedLandmarkList(List<NormalizedLandmarkList> normalizedLandmarkList)
    {
        this.currentNormalizedLandmarkLists = normalizedLandmarkList;
        if (this.handednessLists != null)
        {
            RecognizeGesture(this.currentNormalizedLandmarkLists, this.handednessLists);
            //reset
            this.currentNormalizedLandmarkLists = null;
            this.handednessLists = null;
        }
    }

    public void SetHandednessList(List<ClassificationList> handednessLists)
    {
        this.handednessLists = handednessLists;
        if (this.currentNormalizedLandmarkLists != null)
        {
            RecognizeGesture(this.currentNormalizedLandmarkLists, this.handednessLists);
            //reset
            this.currentNormalizedLandmarkLists = null;
            this.handednessLists = null;
        }
    }

    internal void RecognizeGesture(List<NormalizedLandmarkList> landmarksList, List<ClassificationList> handednessLists)
    {
        List<HandGestureRecognizeData> recognizedSamples = new List<HandGestureRecognizeData>();
        for (int i = 0; i < landmarksList.Count; i++) {
            recognizedSamples.Add(RecognizeLandmarks(landmarksList[i], handednessLists[i]));
        }
        handGestureRecognizedEvent.Invoke(recognizedSamples);
    }
}