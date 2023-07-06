// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

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
        private IList<bool> _currentCleanness;

        public HandGestureAction activateObjectAction;


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
        public void DrawLater(IList<bool> activeness)
        {
            UpdateCurrentTarget(activeness, ref _currentObjectActiveness);
        }
        public void SetCleanness(IList<bool> cleanness)
        {
            UpdateCurrentTarget(cleanness, ref _currentCleanness);
        }

        protected override void SyncNow()
        {
            isStale = false;
            annotation.Draw(_currentHandLandmarkLists, _visualizeZ);

            annotation.MoveObjects(_currentNormalizedRect);

            if (_currentObjectActiveness != null)
                annotation.ActivateObjects(_currentObjectActiveness);
            _currentObjectActiveness = null;

            if (_currentHandedness != null){
                annotation.SetHandedness(_currentHandedness);
            }
            _currentHandedness = null;

            if (_currentCleanness != null)
                annotation.SetCleanness(_currentCleanness);
            _currentCleanness = null;
        }
    }
}
