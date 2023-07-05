// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
    using static UnityEngine.GraphicsBuffer;
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public class RectangleWithLabelListAnnotation : ListAnnotation<RectangleWithLabelAnnotation>
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField, Range(0, 1)] private float _lineWidth = 1.0f;
        [SerializeField, Range(20, 100)] private float _textSize = 1.0f;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this))
            {
                ApplyColor(_color);
                ApplyLineWidth(_lineWidth);
                ApplyTextSize(_textSize);
            }
        }

        private void ApplyTextSize(float textSize)
        {
            foreach (var rect in children)
            {
                if (rect != null) { rect.SetTextSize(textSize); }
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

        public void Draw(IList<Rect> targets, Vector2Int imageSize)
        {
            if (ActivateFor(targets))
            {
                CallActionForAll(targets, (annotation, target) =>
                {
                    if (annotation != null) { annotation.Draw(target, imageSize); }
                });
            }
        }

        public void Draw(IList<NormalizedRect> targets)
        {
            if (ActivateFor(targets))
            {
                CallActionForAll(targets, (annotation, target) =>
                {
                    if (annotation != null) { annotation.Draw(target); }
                });
            }
        }

        public void SetLabels(IList<string> labels)
        {
            if (ActivateFor(labels))
            {
                CallActionForAll(labels, (annotation, labels) =>
                {
                    if (annotation != null) { annotation.SetLabel(labels); }
                });
            }
        }

        protected override RectangleWithLabelAnnotation InstantiateChild(bool isActive = true)
        {
            var annotation = base.InstantiateChild(isActive);
            annotation.SetLineWidth(_lineWidth);
            annotation.SetColor(_color);
            return annotation;
        }

        private void ApplyColor(Color color)
        {
            foreach (var rect in children)
            {
                if (rect != null) { rect.SetColor(color); }
            }
        }

        private void ApplyLineWidth(float lineWidth)
        {
            foreach (var rect in children)
            {
                if (rect != null) { rect.SetLineWidth(lineWidth); }
            }
        }
    }
}
