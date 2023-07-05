using Mediapipe;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Hand gesture/Metric/OneToMany/Max score")]
public class HandGestureOneToManyMaxScoreMetric : HandGestureOneToManyCompareMetric
{
    public HandGestureCompareMetric singleMetric;

    public override float Score(HandLandmarkList landmarks, List<HandGesture> list, out int maxScoreIndex)
    {
        float maxScore = -999;
        int index = -1;

        for (int i = 0; i < list.Count; i++)
        {
            var other = list[i];
            var score = singleMetric.Score(landmarks, other);
            if (maxScore < score)
            {
                index = i;
                maxScore = score;
            }
        }

        maxScoreIndex = index;
        return maxScore;
    }
}