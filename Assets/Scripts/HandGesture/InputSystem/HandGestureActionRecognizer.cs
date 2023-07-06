
using Mediapipe;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Update()
    {
        SyncProcess();
    }

    /// <summary>
    /// This method should run only in the main thread.
    /// </summary>
    public void SyncProcess()
    {
        if (this.currentNormalizedLandmarkLists != null && this.handednessLists != null)
        {
            RecognizeGesture(this.currentNormalizedLandmarkLists, this.handednessLists);
            //reset
            //this.currentNormalizedLandmarkLists = null;
            //this.handednessLists = null;
        }
        else if (this.handednessLists == null && this.currentNormalizedLandmarkLists == null)
        {
            //no data received. Turn off the actions.
            TurnOffAllActions();
        }
    }


    internal HandGestureAction RecognizeActionFromLandmarks(
        NormalizedLandmarkList normalizedLandmarkList,
        ClassificationList handednessList)
    {
        bool isLeft = IsHandLeft(handednessList.Classification);
        int index = -1;

        //re-prepare the samples, maybe user have changed the config.
        var leftSamples = PrepareSamples(true);
        var rightSamples = PrepareSamples(false);
        var samples = (isLeft) ? leftSamples : rightSamples;
        if (samples == null || samples.Count == 0) return HandGestureAction.UNKNOWN;    // no gesture binded? then there's nothing to recognize.

        var recogResult = recognizer.RecognizeLandmarksFromSamples( HandGestureUtility.NormalizeLandmark(normalizedLandmarkList), samples, out index);
        
        if (recogResult != null && recogResult.recognizedSample != null) 
            return HandGestureInputSystem.GetActions()[index];
        else return HandGestureAction.UNKNOWN;
    }

    private List<HandGesture> PrepareSamples(bool isLeft)
    {
        var bindings = HandGestureInputSystem.GetAllBindings();
        var gestures = new List<HandGesture>();
        for (int i = 0; i < bindings.Count; i++) {
            var gesture = (isLeft) ? bindings[i].leftGesture : bindings[i].rightGesture;
            if (gesture != null) gestures.Add(gesture);
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
    }

    public void SetHandednessList(List<ClassificationList> handednessLists)
    {
        this.handednessLists = handednessLists;
    }

    private void TurnOffAllActions()
    {
        HandGestureInputSystem.UpdateActionStatuses(new List<HandGestureAction>());     // empty list
    }

    internal void RecognizeGesture(List<NormalizedLandmarkList> landmarksList, List<ClassificationList> handednessLists)
    {
        List<HandGestureAction> recognizedActions = new List<HandGestureAction>();
        for (int i = 0; i < landmarksList.Count; i++) {
            recognizedActions.Add(RecognizeActionFromLandmarks(landmarksList[i], handednessLists[i]));
        }
        UpdateActionStatuses(recognizedActions);
    }

    private void UpdateActionStatuses(List<HandGestureAction> recognizedActions)
    {
        HandGestureInputSystem.UpdateActionStatuses(recognizedActions);
    }
}