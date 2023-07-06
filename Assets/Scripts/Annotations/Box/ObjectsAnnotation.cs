using Mediapipe.Unity.CoordinateSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public class ObjectsAnnotation : HierarchicalAnnotation
    {
        //public GameObject colliderObject;            // to 'touch' the objects if you want.
        //public GameObject movingObj;                 // to move along the rect
        public List<ObjectAnnotation> objectAnnotations;  
        [SerializeField] private Color _color = Color.red;
        [SerializeField, Range(0, 1)] private float _lineWidth = 1.0f;

        private static readonly Vector3[] _EmptyPositions = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };


        public void Draw(Vector3[] positions)
        {
            Vector3 sum = Vector3.zero;
            for (int i = 0; i < positions.Length; i++)
            {
                sum += positions[i];
            }
            MoveObjectToCenter(sum / positions.Length);
        }
        public void MoveObjectToCenter(Vector3 center)
        {
            foreach (var a in objectAnnotations)    
                a.gameObject.transform.localPosition = center;
        }


        public void Draw(Rect target, Vector2Int imageSize)
        {
            if (ActivateFor(target))
            {
                Draw(GetScreenRect().GetRectVertices(target, imageSize, rotationAngle, isMirrored));
            }
        }

        public void Draw(NormalizedRect target)
        {
            if (ActivateFor(target))
            {
                Draw(GetScreenRect().GetRectVertices(target, rotationAngle, isMirrored));
            }
        }

        internal void SetObjectActive(bool b)
        {
            foreach (var obj in objectAnnotations)
                obj.SetObjectActive(b);
        }
    }
}
