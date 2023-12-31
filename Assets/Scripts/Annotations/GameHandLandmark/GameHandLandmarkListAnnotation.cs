﻿// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity
{
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public sealed class GameHandLandmarkListAnnotation : HierarchicalAnnotation
    {
        [SerializeField] private GamePointListAnnotation _landmarkListAnnotation;
        [SerializeField] private GameConnectionListAnnotation _connectionListAnnotation;
        [SerializeField] private ObjectsAnnotation _objectsAnnotation;
        [SerializeField] private bool _isHandClean;
        [SerializeField] private Hand _handedness;

        [Header("Colors")]
        [SerializeField] private Color _leftLandmarkColor = Color.green;
        [SerializeField] private Color _rightLandmarkColor = Color.green;
        [SerializeField] private Color _cleanLeftHandColor = Color.blue;
        [SerializeField] private Color _cleanRightHandColor = Color.cyan;

        public enum Hand
        {
            Left,
            Right,
        }

        private const int _LandmarkCount = 21;
        private readonly List<(int, int)> _connections = new List<(int, int)> {
      (0, 1),
      (1, 2),
      (2, 3),
      (3, 4),
      (0, 5),
      (5, 9),
      (9, 13),
      (13, 17),
      (0, 17),
      (5, 6),
      (6, 7),
      (7, 8),
      (9, 10),
      (10, 11),
      (11, 12),
      (13, 14),
      (14, 15),
      (15, 16),
      (17, 18),
      (18, 19),
      (19, 20),
    };

        public override bool isMirrored
        {
            set
            {
                _landmarkListAnnotation.isMirrored = value;
                _connectionListAnnotation.isMirrored = value;
                base.isMirrored = value;
            }
        }

        public override RotationAngle rotationAngle
        {
            set
            {
                _landmarkListAnnotation.rotationAngle = value;
                _connectionListAnnotation.rotationAngle = value;
                base.rotationAngle = value;
            }
        }

        public GamePointAnnotation this[int index] => _landmarkListAnnotation[index];

        private void Start()
        {
            _landmarkListAnnotation.Fill(_LandmarkCount);
            _connectionListAnnotation.Fill(_connections, _landmarkListAnnotation);
        }

        public void SetLeftLandmarkColor(Color leftLandmarkColor)
        {
            _leftLandmarkColor = leftLandmarkColor;
        }

        public void SetRightLandmarkColor(Color rightLandmarkColor)
        {
            _rightLandmarkColor = rightLandmarkColor;
        }
        public void SetCleanHandColors(Color leftCleanHandColor, Color rightCleanHandColor)
        {
            _cleanLeftHandColor = leftCleanHandColor;
            _cleanRightHandColor = rightCleanHandColor;
        }

        public void SetLandmarkRadius(float landmarkRadius)
        {
            _landmarkListAnnotation.SetRadius(landmarkRadius);
        }

        public void SetConnectionColor(Color connectionColor)
        {
            _connectionListAnnotation.SetColor(connectionColor);
        }

        public void SetConnectionWidth(float connectionWidth)
        {
            _connectionListAnnotation.SetLineWidth(connectionWidth);
        }

        public void SetHandedness(Hand handedness)
        {
            this._handedness = handedness;
            UpdateColor();
            _objectsAnnotation.SetParams("handedness", (object)handedness);
        }

        private void UpdateColor()
        {
            Color nextColor = _leftLandmarkColor;
            if (this._handedness == Hand.Left)
            {
                nextColor = (_isHandClean) ? _cleanLeftHandColor : _leftLandmarkColor;
            }
            else if (_handedness == Hand.Right)
            {
                nextColor = (_isHandClean) ? _cleanRightHandColor : _rightLandmarkColor;
            }
            _landmarkListAnnotation.SetColor(nextColor);
        }

        public void SetHandedness(IList<Classification> handedness)
        {
            if (handedness == null || handedness.Count == 0 || handedness[0].Label == "Left")
            {
                SetHandedness(Hand.Left);
            }
            else if (handedness[0].Label == "Right")
            {
                SetHandedness(Hand.Right);
            }
            // ignore unknown label
        }

        public void SetHandedness(ClassificationList handedness)
        {
            SetHandedness(handedness.Classification);
        }

        public void Draw(IList<NormalizedLandmark> target, bool visualizeZ = false)
        {
            if (ActivateFor(target))
            {
                _landmarkListAnnotation.Draw(target, visualizeZ);
                // Draw explicitly because connection annotation's targets remain the same.
                _connectionListAnnotation.Redraw();
            }
        }

        public void Draw(NormalizedLandmarkList target, bool visualizeZ = false)
        {
            Draw(target?.Landmark, visualizeZ);
        }

        internal void SetCleanness(bool target)
        {
            _isHandClean = target;
            _landmarkListAnnotation.SetCleanness(target);
        }

        internal void MoveObjects(NormalizedRect target)
        {
            _objectsAnnotation.Draw(target);
        }

        internal void ActivateObject(bool isActive)
        {
            _objectsAnnotation.SetObjectActive(isActive);
        }

    }
}
