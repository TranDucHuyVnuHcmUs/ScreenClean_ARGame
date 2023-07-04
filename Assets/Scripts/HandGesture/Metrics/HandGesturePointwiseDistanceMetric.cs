using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Metric/Pointwise handmark distance")]
public class HandGesturePointwiseDistanceMetric : HandGestureCompareMetric
{
    public float distanceThreshold = 0.2f;

    public override float Score(HandGestureSample sample, HandGestureLandmarkList landmarkList)
    {
        float score = -9999999;
        for (int i = 0; i < sample.landmarkLists.Count; i++)
        {
            float sc = Score(sample.landmarkLists[i], landmarkList);
            score = Mathf.Max(score, sc);       // get the max score?
        }
        return score;
    }

    private float Score(HandGestureLandmarkList handGestureLandmarkList, HandGestureLandmarkList landmarkList)
    {
        float score = 0;
        for (int i = 0; i < handGestureLandmarkList.landmarks.Count; i++)
        {
            score += (Vector3.Distance(handGestureLandmarkList.landmarks[i], landmarkList.landmarks[i]) < distanceThreshold) ? 1 : 0;
        }
        score = score / handGestureLandmarkList.landmarks.Count;
        return score;
    }
}
