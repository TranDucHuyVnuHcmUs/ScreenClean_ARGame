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
    //public class DetectionListUnityEvent : UnityEvent<List<Detection>> { }
    //public class NormalizedRectListUnityEvent : UnityEvent<List<NormalizedRect>> { }
    //public class NormalizedLandmarkListUnityEvent : UnityEvent<List<NormalizedLandmarkList>> { }
    //public class ClassificationListUnityEvent : UnityEvent<List<ClassificationList>> { }


    public class MyHandGestureSolution : ImageSourceSolution<HandTrackingGraph>
    {
        [Header("Annotation controllers")]
        [SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;
        [SerializeField] private RectangleWithLabelListAnnotationController _rectangleWithLabelListAnnotationController;

        [Header("Hand gesture workers")]
        public HandGestureRecorder handGestureRecorder;
        public HandGestureFromPersistenceRecognizer handGestureFromPersistenceRecognizer;

        //events
        [Header("Events")]
        public DetectionListUnityEvent OnPalmDetectionOutputEvent;
        public NormalizedRectListUnityEvent OnPalmDetectionsRectsOutputEvent;
        public NormalizedLandmarkListUnityEvent OnHandLandmarksOutputEvent;
        public NormalizedRectListUnityEvent OnHandLandmarksRectsOutputEvent;
        public ClassificationListUnityEvent OnHandednessOutputEvent;


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
            if (!runningMode.IsSynchronous())
            {
                graphRunner.OnPalmDetectectionsOutput += OnPalmDetectionsOutput;
                graphRunner.OnHandRectsFromPalmDetectionsOutput += OnPalmDetectionsRectsOutput;
                graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
                // TODO: render HandWorldLandmarks annotations
                graphRunner.OnHandRectsFromLandmarksOutput += OnHandLandmarksRectsOutput;
                graphRunner.OnHandednessOutput += OnHandednessOutput;
            }

            var imageSource = ImageSourceProvider.ImageSource;
            SetupAnnotationController(_handLandmarksAnnotationController, imageSource, true);
            if (_handRectsFromLandmarksAnnotationController)
                SetupAnnotationController(_handRectsFromLandmarksAnnotationController, imageSource, true);

            this.handGestureFromPersistenceRecognizer?.handGestureRecognizedEvent.AddListener(OnGestureOutput);
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
        {
            graphRunner.AddTextureFrameToInputStream(textureFrame);
        }

        protected override IEnumerator WaitForNextValue()
        {
            List<Detection> palmDetections = null;
            List<NormalizedRect> handRectsFromPalmDetections = null;
            List<NormalizedLandmarkList> handLandmarks = null;
            List<LandmarkList> handWorldLandmarks = null;
            List<NormalizedRect> handRectsFromLandmarks = null;
            List<ClassificationList> handedness = null;

            if (runningMode == RunningMode.Sync)
            {
                var _ = graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, true);
            }
            else if (runningMode == RunningMode.NonBlockingSync)
            {
                yield return new WaitUntil(() => graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, false));
            }

            _handLandmarksAnnotationController.DrawNow(handLandmarks, handedness);

            if (_rectangleWithLabelListAnnotationController)
            {
                List<string> labels = new List<string>();
                if (handGestureFromPersistenceRecognizer)
                {
                    var recognizeData = handGestureFromPersistenceRecognizer.RecognizeGestureReturn(handLandmarks, handedness);
                    foreach (var data in recognizeData)
                    {
                        string label = (data.recognizedSample != null ? data.recognizedSample.gestureName : "???");
                        labels.Add(label + ":" + data.score.ToString());
                    }
                }
                else labels = new List<string>(handRectsFromLandmarks.Count);

                _rectangleWithLabelListAnnotationController.DrawNow(handRectsFromLandmarks, labels);
            }
            
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
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
            if (eventArgs.value != null)
            {
                handGestureRecorder?.SetLandmarks(eventArgs.value[0]);       // we only care about the first hand.
                //handGestureRecognizer?.RecognizeGesture(eventArgs.value);
                handGestureFromPersistenceRecognizer?.SetNormalizedLandmarkList(eventArgs.value);
            }
        }

        private void OnHandLandmarksRectsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            this.OnHandLandmarksRectsOutputEvent.Invoke(eventArgs.value);
            _handRectsFromLandmarksAnnotationController?.DrawLater(eventArgs.value);
            _rectangleWithLabelListAnnotationController?.DrawLater(eventArgs.value);
        }

        private void OnHandednessOutput(object stream, OutputEventArgs<List<ClassificationList>> eventArgs)
        {
            this.OnHandednessOutputEvent.Invoke(eventArgs.value);
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
            if (eventArgs.value != null)
            {
                handGestureFromPersistenceRecognizer?.SetHandednessList(eventArgs.value);
                handGestureRecorder?.SetHandedness(eventArgs.value[0].Classification);
            }
        }

        public void OnGestureOutput(List<HandGestureRecognizeData> recognizeDatas)
        {
            List<string> labels = new List<string>();
            foreach (var data in recognizeDatas)
            {
                string label = (data.recognizedSample != null ? data.recognizedSample.gestureName : "???");
                labels.Add(label + ":" + data.score.ToString());
            }
            _rectangleWithLabelListAnnotationController.DrawLater(labels);
        }
    }
}
