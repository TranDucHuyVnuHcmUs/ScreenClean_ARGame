using Mediapipe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HandGestureRecorder : MonoBehaviour
{
    public bool isRecording = false;
    public bool isLeftHandRecorded = false;
    private HandGesture recordingSample;
    public HandGesturePersistenceObject storageObj;

    private void Start()
    {
        recordingSample = new HandGesture();
    }

    public void SetHandedness(IList<Classification> clfs)
    {
        this.isLeftHandRecorded = CheckHandedness(clfs);    
    }

    private bool CheckHandedness(IList<Classification> clfs)
    {
        if (clfs == null || clfs.Count == 0 || clfs[0].Label == "Left") return true;
        else return false;
    }

    internal void SetLandmarks(NormalizedLandmarkList landmarks)
    {
        if (isRecording)
        {
            var normlandmarks = HandGestureUtility.NormalizeLandmark(landmarks);
            recordingSample.landmarkLists.Add(normlandmarks);          // collect as many landmark set as possible, so as to increase accuracy later on!
        }
    }

    internal void StartRecording(string gestureName)
    {
        isRecording = true;
        recordingSample = new HandGesture(gestureName, this.isLeftHandRecorded);
        recordingSample.gestureName = gestureName;
    }

    public void StopRecording()
    {
        isRecording = false;
        SaveRecordedLandmarks();
    }

    //use some kind of file saving?
    private void SaveRecordedLandmarks()
    {
         this.storageObj.SaveToPersistence(
             this.recordingSample.gestureName + ".json", 
             this.recordingSample);
    }

}
