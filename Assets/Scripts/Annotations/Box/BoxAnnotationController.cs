// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;

namespace Mediapipe.Unity
{
    public class BoxAnnotationController : AnnotationController<BoxListAnnotation>
    {
        private IList<NormalizedRect> _currentTarget;
        public bool isOn = false;

        public void Activate()
        {
            this.isOn = true;
            this.annotation.gameObject.SetActive(isOn);
        }

        public void Disable()
        {
            this.isOn = false;
            this.annotation.gameObject.SetActive(false);    
        }

        public void DrawNow(IList<NormalizedRect> target)
        {
            _currentTarget = target;
            if (isOn)
                SyncNow();
        }

        public void DrawLater(IList<NormalizedRect> target)
        {
            if (isOn)
                UpdateCurrentTarget(target, ref _currentTarget);
        }

        protected override void SyncNow()
        {
            isStale = false;
            if (isOn)
                annotation.Draw(_currentTarget);
        }
    }
}
