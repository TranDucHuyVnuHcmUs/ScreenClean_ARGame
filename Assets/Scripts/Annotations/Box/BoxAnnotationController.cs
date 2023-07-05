// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;

namespace Mediapipe.Unity
{
    public class BoxListAnnotationController : AnnotationController<BoxListAnnotation>
    {
        private IList<NormalizedRect> _currentTarget;
        private IList<bool> _isOn;


        public void DrawNow(IList<NormalizedRect> target)
        {
            _currentTarget = target;
            SyncNow();
        }

        public void DrawLater(IList<NormalizedRect> target)
        {
            UpdateCurrentTarget(target, ref _currentTarget);
        }
        public void SetBools(IList<bool> target)
        {
            UpdateCurrentTarget(target, ref _isOn);
        }

        protected override void SyncNow()
        {
            isStale = false;
            annotation.Draw(_currentTarget);
            annotation.SetObjectsActive(_isOn);
        }

    }
}
