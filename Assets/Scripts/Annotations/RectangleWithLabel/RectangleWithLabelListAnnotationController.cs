// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections.Generic;

namespace Mediapipe.Unity
{
    public class RectangleWithLabelListAnnotationController : AnnotationController<RectangleWithLabelListAnnotation>
    {
        private IList<NormalizedRect> _currentTarget;
        private IList<string> _currentLabels;

        public void DrawNow(IList<NormalizedRect> target, IList<string> labels)
        {
            _currentTarget = target;
            _currentLabels = labels;
            SyncNow();
        }

        public void DrawLater(IList<NormalizedRect> target)
        {
            UpdateCurrentTarget(target, ref _currentTarget);
        }
        public void DrawLater(IList<string> labels)
        {
            UpdateCurrentTarget(labels, ref _currentLabels);
        }

        protected override void SyncNow()
        {
            isStale = false;
            annotation.Draw(_currentTarget);
            annotation.SetLabels(_currentLabels);
        }
    }
}
