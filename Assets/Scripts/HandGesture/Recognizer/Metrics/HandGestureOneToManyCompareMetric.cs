using System.Collections.Generic;
using UnityEngine;

public abstract class HandGestureOneToManyCompareMetric : ScriptableObject
{
    /// <summary>
    /// This function returns the maximum score based on the given metric (as well as the index (0-based) of the gesture (in list) that are most likely with the sample.
    /// </summary>
    /// <param name="sample"></param>
    /// <param name="list"></param>
    /// <param name="maxScoreIndex">The index of the gesture in list that is most similar to the sample.</param>
    /// <returns></returns>
    public abstract float Score(HandLandmarkList landmarks, List<HandGesture> list, out int maxScoreIndex);
}