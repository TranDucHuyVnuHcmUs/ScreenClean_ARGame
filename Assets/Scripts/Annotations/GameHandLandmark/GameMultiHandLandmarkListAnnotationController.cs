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
    public class GameMultiHandLandmarkListAnnotationController : AnnotationController<GameMultiHandLandmarkListAnnotation>
    {
        [SerializeField] private bool _visualizeZ = false;

        private IList<NormalizedLandmarkList> _currentHandLandmarkLists;
        private IList<ClassificationList> _currentHandedness;

        private IList<NormalizedRect> _currentNormalizedRect;
        private IList<bool> _currentObjectActiveness;
        //private IList<bool> _currentCleanness;

        public HandGestureAction activateObjectAction;

        public GameMultiHandLandmarkListAnnotationConfig config;

        protected override void Start()
        {
            base.Start();
            InitializeFromConfig(this.config);
        }

        private void InitializeFromConfig(GameMultiHandLandmarkListAnnotationConfig config)
        {
            annotation.SetConfig(config);
        }

        public void DrawNow(IList<NormalizedLandmarkList> handLandmarkLists, IList<ClassificationList> handedness = null)
        {
            _currentHandLandmarkLists = handLandmarkLists;
            _currentHandedness = handedness;
            SyncNow();
        }

        public void DrawLater(IList<NormalizedLandmarkList> handLandmarkLists)
        {
            UpdateCurrentTarget(handLandmarkLists, ref _currentHandLandmarkLists);
        }

        public void DrawLater(IList<ClassificationList> handedness)
        {
            UpdateCurrentTarget(handedness, ref _currentHandedness);
        }
        public void DrawLater(IList<NormalizedRect> normalizedRects)
        {
            UpdateCurrentTarget(normalizedRects, ref _currentNormalizedRect);
        }
        public void DrawLaterActiveness(IList<bool> activeness)
        {
            UpdateCurrentTarget(activeness, ref _currentObjectActiveness);
        }

        protected override void SyncNow()
        {
            isStale = false;
            annotation.Draw(_currentHandLandmarkLists, _visualizeZ);
            //_currentHandLandmarkLists = null;

            annotation.MoveObjects(_currentNormalizedRect);
            //_currentNormalizedRect = null;

            if (_currentObjectActiveness != null)
                annotation.ActivateObjects(_currentObjectActiveness);
            _currentObjectActiveness = null;

            if (_currentHandedness != null){
                annotation.SetHandedness(_currentHandedness);
            }
            _currentHandedness = null;
        }
    }
}
