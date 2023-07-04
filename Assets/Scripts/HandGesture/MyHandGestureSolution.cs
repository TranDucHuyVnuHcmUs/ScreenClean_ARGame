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

namespace Mediapipe.Unity.HandTracking
{
    public class MyHandGestureSolution : ImageSourceSolution<HandTrackingGraph>
    {
        [SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
        [SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;
        [SerializeField] private RectangleWithLabelListAnnotationController _rectangleWithLabelListAnnotationController;
        public HandGestureRecorder handGestureRecorder;
        public HandGestureRecognizer handGestureRecognizer;

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
                graphRunner.OnHandRectsFromPalmDetectionsOutput += OnHandRectsFromPalmDetectionsOutput;
                graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
                // TODO: render HandWorldLandmarks annotations
                graphRunner.OnHandRectsFromLandmarksOutput += OnHandRectsFromLandmarksOutput;
                graphRunner.OnHandednessOutput += OnHandednessOutput;
            }

            var imageSource = ImageSourceProvider.ImageSource;
            SetupAnnotationController(_handLandmarksAnnotationController, imageSource, true);
            SetupAnnotationController(_handRectsFromLandmarksAnnotationController, imageSource, true);

            this.handGestureRecognizer?.handGestureRecognizedEvent.AddListener(OnGestureOutput);
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

            List<string> labels = new List<string>();
            if (handGestureRecognizer) {
                var recognizeData = handGestureRecognizer.RecognizeGestureSync(handLandmarks);
                foreach (var data in recognizeData) {
                    string label = (data.recognizedSample != null ? data.recognizedSample.gestureName : "???");
                    labels.Add(label + ":" + data.score.ToString()); 
                }
            } else labels = new List<string>(handRectsFromLandmarks.Count);

            _rectangleWithLabelListAnnotationController.DrawNow(handRectsFromLandmarks, labels);
        }

        private void OnPalmDetectionsOutput(object stream, OutputEventArgs<List<Detection>> eventArgs)
        {
        }

        private void OnHandRectsFromPalmDetectionsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
        }

        private void OnHandLandmarksOutput(object stream, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)

        {
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
            if (eventArgs.value != null)
            {
                handGestureRecorder?.GetLandmarks(eventArgs.value[0]);       // we only care about the first hand.
                handGestureRecognizer?.GetLandmarks(eventArgs.value);
            }
        }

        private void OnHandRectsFromLandmarksOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
        {
            //_handRectsFromLandmarksAnnotationController.DrawLater(eventArgs.value);
            _rectangleWithLabelListAnnotationController.DrawLater(eventArgs.value);
        }

        private void OnHandednessOutput(object stream, OutputEventArgs<List<ClassificationList>> eventArgs)
        {
            _handLandmarksAnnotationController.DrawLater(eventArgs.value);
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
