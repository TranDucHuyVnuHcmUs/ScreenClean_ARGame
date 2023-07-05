
using Mediapipe;
using System;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This recognizer wrap the original for more specific usecase of finding gestures from gesture data persistence.  
/// </summary>
public class HandGestureActionRecognizer : MonoBehaviour
{
    public HandGesturePersistenceObject handGesturePersistentStorageObj;
    public HandGestureInputSystem handGestureInputSystem;
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
        handGestureInputSystem = HandGestureInputSystem.instance;
        handGesturePersistentStorageObj.Initialize();           // ask it to fill the data
    }

    internal HandGestureAction RecognizeActionFromLandmarks(
        NormalizedLandmarkList normalizedLandmarkList,
        ClassificationList handednessList)
    {
        bool isLeft = IsHandLeft(handednessList.Classification);
        var samples = PrepareSamples(isLeft);
        int index = -1;
        var recogResult = recognizer.RecognizeLandmarksFromSamples( HandGestureUtility.NormalizeLandmark(normalizedLandmarkList), samples, out index);
        
        if (recogResult != null && recogResult.recognizedSample != null) 
            return HandGestureInputSystem.GetActions()[index];
        else return HandGestureAction.UNKNOWN;
    }

    private List<HandGesture> PrepareSamples(bool isLeft)
    {
        var bindings = HandGestureInputSystem.GetAllBindings();
        var gestures = new List<HandGesture>(bindings.Count);
        for (int i = 0; i < bindings.Count; i++) {
            gestures[i] = (isLeft) ? bindings[i].leftGesture : bindings[i].rightGesture;
        }
        return gestures;
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
        List<HandGestureAction> recognizedActions = new List<HandGestureAction>();
        for (int i = 0; i < landmarksList.Count; i++) {
            recognizedActions.Add(RecognizeActionFromLandmarks(landmarksList[i], handednessLists[i]));
        }
        TriggerRecognizedActions(recognizedActions);
    }

    private void TriggerRecognizedActions(List<HandGestureAction> recognizedActions)
    {
        HandGestureInputSystem.TriggerManyStartEvent(recognizedActions);
    }
}