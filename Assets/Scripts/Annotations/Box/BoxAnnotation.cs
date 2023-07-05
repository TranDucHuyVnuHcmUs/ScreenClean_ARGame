// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.Unity.CoordinateSystem;
using System;
using UnityEngine;

using mplt = Mediapipe.LocationData.Types;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public class BoxAnnotation : HierarchicalAnnotation
    {
        public GameObject box;
        [SerializeField] private Color _color = Color.red;
        [SerializeField, Range(0, 1)] private float _lineWidth = 1.0f;

        private static readonly Vector3[] _EmptyPositions = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        private void OnEnable()
        {
            ApplyColor(_color);
        }

        private void OnDisable()
        {
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this))
            {
                ApplyColor(_color);
            }
        }
#endif

        public void SetColor(Color color)
        {
            _color = color;
            ApplyColor(_color);
        }

        public void SetLineWidth(float lineWidth)
        {
            _lineWidth = lineWidth;
        }

        public void Draw(Vector3[] positions)
        {
            Vector3 sum = Vector3.zero;
            for (int i = 0; i < positions.Length; i++)
            {
                sum += positions[i];
            }
            MoveBoxToCenter(sum / positions.Length);
        }
        public void MoveBoxToCenter(Vector3 center)
        {
            box.transform.localPosition = center;
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

        public void Draw(LocationData target, Vector2Int imageSize)
        {
            if (ActivateFor(target))
            {
                switch (target.Format)
                {
                    case mplt.Format.BoundingBox:
                        {
                            Draw(GetScreenRect().GetRectVertices(target.BoundingBox, imageSize, rotationAngle, isMirrored));
                            break;
                        }
                    case mplt.Format.RelativeBoundingBox:
                        {
                            Draw(GetScreenRect().GetRectVertices(target.RelativeBoundingBox, rotationAngle, isMirrored));
                            break;
                        }
                    case mplt.Format.Global:
                    case mplt.Format.Mask:
                    default:
                        {
                            throw new System.ArgumentException($"The format of the LocationData must be BoundingBox or RelativeBoundingBox, but {target.Format}");
                        }
                }
            }
        }

        public void Draw(LocationData target)
        {
            if (ActivateFor(target))
            {
                switch (target.Format)
                {
                    case mplt.Format.RelativeBoundingBox:
                        {
                            Draw(GetScreenRect().GetRectVertices(target.RelativeBoundingBox, rotationAngle, isMirrored));
                            break;
                        }
                    case mplt.Format.BoundingBox:
                    case mplt.Format.Global:
                    case mplt.Format.Mask:
                    default:
                        {
                            throw new System.ArgumentException($"The format of the LocationData must be RelativeBoundingBox, but {target.Format}");
                        }
                }
            }
        }

        private void ApplyColor(Color color)
        {
                
        }

        internal void SetObjectActive(bool b)
        {
            this.box.SetActive(b);
        }
    }
}
