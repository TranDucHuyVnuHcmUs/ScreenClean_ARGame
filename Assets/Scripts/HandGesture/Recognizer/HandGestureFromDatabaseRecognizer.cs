
using Mediapipe;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This recognizer wrap the original for more specific usecase of finding gestures from gesture data persistence.  
/// </summary>
public class HandGestureFromDatabaseRecognizer : MonoBehaviour
{
    public HandGesturePersistentStorageObj handGesturePersistentStorageObj;
    public HandGestureRecognizer recognizer;

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

    internal HandGestureRecognizeData RecognizeLandmarksFromDatabase(NormalizedLandmarkList normalizedLandmarkList,
        ClassificationList handednessList)
    {
        bool isLeft = IsHandLeft(handednessList.Classification);
        List<HandGesture> samples = (isLeft) ? handGesturePersistentStorageObj.leftSamples : handGesturePersistentStorageObj.rightSamples;

        return recognizer.RecognizeLandmarksFromSamples( HandGestureUtility.NormalizeLandmark(normalizedLandmarkList), samples);
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
            recognizedSamples.Add(RecognizeLandmarksFromDatabase(landmarksLists[i], handednessLists[i]));
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
        for (int i = 0; i < landmarksList.Count; i++)
        {
            recognizedSamples.Add(RecognizeLandmarksFromDatabase(landmarksList[i], handednessLists[i]));
        }
        handGestureRecognizedEvent.Invoke(recognizedSamples);
    }
}