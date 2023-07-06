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
#pragma warning disable IDE0065
    using Color = UnityEngine.Color;
#pragma warning restore IDE0065

    public class ObjectsListAnnotation : ListAnnotation<ObjectsAnnotation>
    {

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
        internal void SetObjectsActive(IList<bool> targets)
        {
            if (ActivateFor(targets))
            {
                CallActionForAll(targets, (annotation, target) =>
                {
                    if (annotation != null) { annotation.SetObjectActive(target); }
                });
            }
        }

        protected override ObjectsAnnotation InstantiateChild(bool isActive = true)
        {
            var annotation = base.InstantiateChild(isActive);
            return annotation;
        }

    }
}
