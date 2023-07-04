using Mediapipe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HandGestureRecorder : MonoBehaviour
{
    public bool isRecording = false;
    private HandGestureSample recordingSample;
    public HandGesturePersistentStorageObj storageObj;

    private void Start()
    {
        recordingSample = new HandGestureSample();
    }

    internal void GetLandmarks(NormalizedLandmarkList landmarks)
    {
        if (isRecording)
        {
            var normlandmarks = NormalizeLandmark(landmarks);
            recordingSample.landmarkLists.Add(normlandmarks);          // collect as many landmark set as possible, so as to increase accuracy later on!
        }
    }

    internal void StartRecording()
    {
        isRecording = true;
        recordingSample = new HandGestureSample();
    }
    internal void StartRecording(string gestureName)
    {
        StartRecording();
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
             this.recordingSample.gestureName + "_" + this.recordingSample.recordDate + ".json", 
             JsonUtility.ToJson(this.recordingSample));
    }

    private HandGestureLandmarkList NormalizeLandmark(NormalizedLandmarkList landmarks)
    {
        float inf = 9999999;
        float minX = inf, minY = inf, minZ = inf, maxX = -inf, maxY = -inf, maxZ = -inf;
        for (int i = 0; i < landmarks.Landmark.Count; i++)
        {
            minX = Mathf.Min(minX, landmarks.Landmark[i].X);
            maxX = Mathf.Max(maxX, landmarks.Landmark[i].X);
            
            minY = Mathf.Min(minY, landmarks.Landmark[i].Y);
            maxY = Mathf.Max(maxY, landmarks.Landmark[i].Y);

            minZ = Mathf.Min(minZ, landmarks.Landmark[i].Z);
            maxZ = Mathf.Max(maxZ, landmarks.Landmark[i].Z);
        }
        
        List<Vector3> landmark_vecs = new List<Vector3>();

        for (int i = 0; i < landmarks.Landmark.Count; i++)
        {
            landmark_vecs.Add(new Vector3(
                (landmarks.Landmark[i].X - minX) / (maxX - minX),
                (landmarks.Landmark[i].Y - minY) / (maxY - minY),
                (landmarks.Landmark[i].Z - minZ) / (maxZ - minZ)
            ));
        }

        //Debug.Log(string.Format("Bounding box: min:({0},{1},{2}), max:({3},{4},{5})", minX, minY, minZ, maxX, maxY, maxZ));
        return new HandGestureLandmarkList(landmark_vecs);
    }

}
