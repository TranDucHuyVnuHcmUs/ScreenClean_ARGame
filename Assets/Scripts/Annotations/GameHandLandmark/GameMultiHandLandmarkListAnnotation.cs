
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

    public sealed class GameMultiHandLandmarkListAnnotation : ListAnnotation<GameHandLandmarkListAnnotation>
    {
        [SerializeField] private GameMultiHandLandmarkListAnnotationConfig config;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!UnityEditor.PrefabUtility.IsPartOfAnyPrefab(this))
            {
                ApplyLeftLandmarkColor(config._leftLandmarkColor);
                ApplyRightLandmarkColor(config._rightLandmarkColor);
                ApplyLandmarkRadius(config._landmarkRadius);
                ApplyConnectionColor(config._connectionColor);
                ApplyConnectionWidth(config._connectionWidth);
            }
        }
#endif


        public void SetHandedness(IList<ClassificationList> handedness)
        {
            var count = handedness == null ? 0 : handedness.Count;
            for (var i = 0; i < Mathf.Min(count, children.Count); i++)
            {
                children[i].SetHandedness(handedness[i]);
            }
            for (var i = count; i < children.Count; i++)
            {
                children[i].SetHandedness((IList<Classification>)null);
            }
        }

        public void Draw(IList<NormalizedLandmarkList> targets, bool visualizeZ = false)
        {
            if (ActivateFor(targets))
            {
                CallActionForAll(targets, (annotation, target) =>
                {
                    if (annotation != null) { annotation.Draw(target, visualizeZ); }
                });
            }
        }

        protected override GameHandLandmarkListAnnotation InstantiateChild(bool isActive = true)
        {
            var annotation = base.InstantiateChild(isActive);
            annotation.SetLeftLandmarkColor(config._leftLandmarkColor);
            annotation.SetRightLandmarkColor(config._rightLandmarkColor);
            annotation.SetCleanHandColors(config._cleanLeftHandColor, config._cleanRightHandColor);

            annotation.SetLandmarkRadius(config._landmarkRadius);
            annotation.SetConnectionColor(config._connectionColor);
            annotation.SetConnectionWidth(config._connectionWidth);
            return annotation;
        }

        private void ApplyLeftLandmarkColor(Color leftLandmarkColor)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetLeftLandmarkColor(leftLandmarkColor); }
            }
        }

        private void ApplyRightLandmarkColor(Color rightLandmarkColor)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetRightLandmarkColor(rightLandmarkColor); }
            }
        }
        private void ApplyCleanHandColors(Color cleanLeftHandColor, Color cleanRightHandColor)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetCleanHandColors(cleanLeftHandColor, cleanRightHandColor); }
            }
        }


        private void ApplyLandmarkRadius(float landmarkRadius)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetLandmarkRadius(landmarkRadius); }
            }
        }

        private void ApplyConnectionColor(Color connectionColor)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetConnectionColor(connectionColor); }
            }
        }

        private void ApplyConnectionWidth(float connectionWidth)
        {
            foreach (var handLandmarkList in children)
            {
                if (handLandmarkList != null) { handLandmarkList.SetConnectionWidth(connectionWidth); }
            }
        }


        internal void MoveObjects(IList<NormalizedRect> currentNormalizedRect)
        {
            if (ActivateFor(currentNormalizedRect))
            {
                CallActionForAll(currentNormalizedRect, (annotation, target) =>
                {
                    if (annotation != null) { annotation.MoveObjects(target); }
                });
            }
        }

        internal void ActivateObjects(IList<bool> currentObjectActiveness)
        {
            //if (ActivateFor(currentObjectActiveness))
            {
                CallActionForAll(currentObjectActiveness, (annotation, target) =>
                {
                    if (annotation != null) { annotation.ActivateObject(target); }
                });
            }
        }

        internal void SetConfig(GameMultiHandLandmarkListAnnotationConfig config)
        {
            this.config = config;
        }
    }
}
