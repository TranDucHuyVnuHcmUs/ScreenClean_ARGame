// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using Mediapipe.Unity.CoordinateSystem;
using System;
using UnityEngine;
using UnityEngine.UI;
using mplt = Mediapipe.LocationData.Types;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public class RectangleWithLabelAnnotation : HierarchicalAnnotation
    {
        public Text labelText;
        public GameObject labelUI;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Color _color = Color.red;
        [SerializeField, Range(0, 1)] private float _lineWidth = 1.0f;
        [SerializeField] private string _label;

        private static readonly Vector3[] _EmptyPositions = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };

        private void OnEnable()
        {
            ApplyColor(_color);
            ApplyLineWidth(_lineWidth);
        }

        private void ApplyLabel(string label)
        {
            labelText.text = label;
        }

        private void OnDisable()
        {
            ApplyLineWidth(0.0f);
            _lineRenderer.SetPositions(_EmptyPositions);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this))
            {
                ApplyColor(_color);
                ApplyLineWidth(_lineWidth);
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
            ApplyLineWidth(_lineWidth);
        }

        public void Draw(Vector3[] positions)
        {
            _lineRenderer.SetPositions(positions ?? _EmptyPositions);
            labelUI.transform.localPosition = positions[1];
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
            if (_lineRenderer != null)
            {
                _lineRenderer.startColor = color;
                _lineRenderer.endColor = color;
            }
        }

        private void ApplyLineWidth(float lineWidth)
        {
            if (_lineRenderer != null)
            {
                _lineRenderer.startWidth = lineWidth;
                _lineRenderer.endWidth = lineWidth;
            }
        }

        internal void SetLabel(string label)
        {
            if (label != null)
                this.labelText.text = label;
            else this.labelText.text = "???";
        }

        internal void SetTextSize(float textSize)
        {
            this.labelText.fontSize = (int)textSize;
        }
    }
}
