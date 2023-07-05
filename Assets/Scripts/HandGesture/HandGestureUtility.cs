using Mediapipe;
using System.Collections.Generic;
using UnityEngine;

public class HandGestureUtility
{
    public static HandLandmarkList NormalizeLandmark(NormalizedLandmarkList landmarks)
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
        return new HandLandmarkList(landmark_vecs);
    }

}