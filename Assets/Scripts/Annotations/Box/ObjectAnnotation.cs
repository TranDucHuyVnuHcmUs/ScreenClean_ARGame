using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
    public class ObjectAnnotation : MonoBehaviour
    {
        public GameObject obj;

        private Dictionary<string, object> parameters;

        private void Awake()
        {
            parameters = new Dictionary<string, object>();
        }

        internal virtual void SetObjectActive(bool b)
        {
            obj.SetActive(b);
        }

        internal void SetParams(string key, object value)
        {
            this.parameters[key] = value;
        }
    }
}