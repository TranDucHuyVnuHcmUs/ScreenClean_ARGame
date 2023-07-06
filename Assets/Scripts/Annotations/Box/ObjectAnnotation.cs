using System;
using UnityEngine;

namespace Mediapipe.Unity
{
    public class ObjectAnnotation : MonoBehaviour
    {
        public GameObject obj;
        internal virtual void SetObjectActive(bool b)
        {
            obj.SetActive(b);
        }
    }
}