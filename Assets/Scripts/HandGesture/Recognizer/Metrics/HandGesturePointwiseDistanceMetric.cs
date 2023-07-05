using Mediapipe;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hand gesture/Metric/Single/Pointwise handmark distance")]
public class HandGesturePointwiseDistanceMetric : HandGestureCompareMetric
{
    public float distanceThreshold = 0.2f;

    public override float Score(HandLandmarkList landmarks, HandGesture sample)
    {
        float score = -9999999;
        for (int i = 0; i < sample.landmarkLists.Count; i++)
        {
            float sc = Score(landmarks, sample.landmarkLists[i]);
            score = Mathf.Max(score, sc);       // get the max score?
        }
        return score;
    }

    private float Score(HandLandmarkList handLandmarkList, HandLandmarkList landmarkList)
    {
        float score = 0;
        for (int i = 0; i < handLandmarkList.landmarks.Count; i++)
        {
            score += (Vector3.Distance(handLandmarkList.landmarks[i], landmarkList.landmarks[i]) < distanceThreshold) ? 1 : 0;
        }
        score = score / handLandmarkList.landmarks.Count;
        return score;
    }
}
