using Mediapipe;
using UnityEngine;

public abstract class HandGestureCompareMetric : ScriptableObject
{
    public float Score(HandGestureSample sample, NormalizedLandmarkList landmarkList)
    {
        return Score(sample, HandGestureUtility.NormalizeLandmark(landmarkList));
    }

    public abstract float Score(HandGestureSample sample, HandGestureLandmarkList landmarkList);
}