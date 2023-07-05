// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

namespace Mediapipe.Unity.HandTracking
{
    public class MyHandTrackingSolutionOutput
    {
        public List<Detection> palmDetections;
        public List<NormalizedRect> palmRects;
        public List<NormalizedLandmarkList> handLandmarks;
        public List<LandmarkList> handWorldLandmarks;
        public List<NormalizedRect> handLandmarksRects;
        public List<ClassificationList> handedness;

        public MyHandTrackingSolutionOutput(
            List<Detection> palmDetections, 
            List<NormalizedRect> palmRects, 
            List<NormalizedLandmarkList> handLandmarks, 
            List<LandmarkList> handWorldLandmarks, 
            List<NormalizedRect> handLandmarksRects, 
            List<ClassificationList> handedness)
        {
            this.palmDetections = palmDetections;
            this.palmRects = palmRects;
            this.handLandmarks = handLandmarks;
            this.handWorldLandmarks = handWorldLandmarks;
            this.handLandmarksRects = handLandmarksRects;
            this.handedness = handedness;
        }
    }

    public class DetectionListUnityEvent : UnityEvent<List<Detection>> { }
    public class NormalizedRectListUnityEvent : UnityEvent<List<NormalizedRect>> { }
    public class NormalizedLandmarkListUnityEvent : UnityEvent<List<NormalizedLandmarkList>> { }
    public class ClassificationListUnityEvent : UnityEvent<List<ClassificationList>> { }
    public class MyHandTrackingSolutionOutputUnityEvent : UnityEvent<MyHandTrackingSolutionOutput> {}


    public class MyHandTrackingSolution : ImageSourceSolution<HandTrackingGraph>
    {
        //[Header("Annotation controllers")]
        //[SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
        //[SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;
        //[SerializeField] private RectangleWithLabelListAnnotationController _rectangleWithLabelListAnnotationController;

        //[Header("Hand gesture workers")]
        //public HandGestureRecorder handGestureRecorder;
        //public HandGestureFromPersistenceRecognizer handGestureFromPersistenceRecognizer;

        //events
        [Header("Events")]
        public UnityEvent OnStartRunEvent;

        public DetectionListUnityEvent OnPalmDetectionOutputEvent;
        public NormalizedRectListUnityEvent OnPalmDetectionsRectsOutputEvent;
        public NormalizedLandmarkListUnityEvent OnHandLandmarksOutputEvent;
        public NormalizedRectListUnityEvent OnHandLandmarksRectsOutputEvent;
        public ClassificationListUnityEvent OnHandednessOutputEvent;

        public MyHandTrackingSolutionOutputUnityEvent OnAllOutputSyncEvent;

        private void Awake()
        {
            OnStartRunEvent = new UnityEvent();

            OnPalmDetectionOutputEvent = new DetectionListUnityEvent();
            OnPalmDetectionsRectsOutputEvent = new NormalizedRectListUnityEvent();
            OnHandLandmarksOutputEvent = new NormalizedLandmarkListUnityEvent();
            OnHandLandmarksRectsOutputEvent = new NormalizedRectListUnityEvent();
            OnHandednessOutputEvent = new ClassificationListUnityEvent();

            OnAllOutputSyncEvent = new MyHandTrackingSolutionOutputUnityEvent();
        }

        public HandTrackingGraph.ModelComplexity modelComplexity
        {
            get => graphRunner.modelComplexity;
            set => graphRunner.modelComplexity = value;
        }

        public int maxNumHands
        {
            get => graphRunner.maxNumHands;
            set => graphRunner.maxNumHands = value;
        }

        public float minDetectionConfidence
        {
            get => graphRunner.minDetectionConfidence;
            set => graphRunner.minDetectionConfidence = value;
        }

        public float minTrackingConfidence
        {
            get => graphRunner.minTrackingConfidence;
            set => graphRunner.minTrackingConfidence = value;
        }

        protected override void OnStartRun()
        {
            InitializeEvents();
            OnStartRunEvent.Invoke();
        }

        private void InitializeEvents()
        {
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPalmDetectectionsOutput += OnPalmDetectionsOutput;
                graphRunner.OnHandRectsFromPalmDetectionsOutput += OnPalmDetectionsRectsOutput;
                graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
                // TODO: render HandWorldLandmarks annotations
                graphRunner.OnHandRectsFromLandmarksOutput += OnHandLandmarksRectsOutput;
                graphRunner.OnHandednessOutput += OnHandednessOutput;
            }
        }

        internal void SetupAnnotationControllerPublic<T>(
            AnnotationController<T> annotationController, 
            ImageSource imageSource, 
            bool expectedToBeMirrored = false) 
                where T : HierarchicalAnnotation
        {
            annotationController.isMirrored = expectedToBeMirrored ^ imageSource.isHorizontallyFlipped ^ imageSource.isFrontFacing;
            annotationController.rotationAngle = imageSource.rotation.Reverse();
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
        {
            graphRunner.AddTextureFrameToInputStream(textureFrame);
        }

        protected override IEnumerator WaitForNextValue()
        {
            List<Detection> palmDetections = null;
            List<NormalizedRect> palmRects = null;
            List<NormalizedLandmarkList> handLandmarks = null;
            List<LandmarkList> handWorldLandmarks = null;
            List<NormalizedRect> handLandmarksRects = null;
            List<ClassificationList> handedness = null;

            if (runningMode == RunningMode.Sync)
            {
                var _ = graphRunner.TryGetNext(out palmDetections, out palmRects, out handLandmarks, out handWorldLandmarks, out handLandmarksRects, out handedness, true);
            }
            else if (runningMode == RunningMode.NonBlockingSync)
            {
                yield return new WaitUntil(() => graphRunner.TryGetNext(out palmDetections, out palmRects, out handLandmarks, out handWorldLandmarks, out handLandmarksRects, out handedness, false));
            }

            OnAllOutputSyncEvent.Invoke(new MyHandTrackingSolutionOutput(palmDetections, palmRects,
                handLandmarks, handWorldLandmarks, handLandmarksRects, handedness));

        }

        private void OnPalmDetectionsOutput(object stream, OutputEventArgs<List<Detection>> eventArgs)
        {
            this.OnPalmDetectionOutputEvent.Invoke(eventArgs.value);
        }

        private void OnPalmDetectionsRectsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            this.OnPalmDetectionsRectsOutputEvent.Invoke(eventArgs.value);
        }

        private void OnHandLandmarksOutput(object stream, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
        {
            this.OnHandLandmarksOutputEvent.Invoke(eventArgs.value);
        }

        private void OnHandLandmarksRectsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            this.OnHandLandmarksRectsOutputEvent.Invoke(eventArgs.value);
        }

        private void OnHandednessOutput(object stream, OutputEventArgs<List<ClassificationList>> eventArgs)
        {
            this.OnHandednessOutputEvent.Invoke(eventArgs.value);
        }

    }
}
