using System;
using UnityEngine;

namespace Mediapipe.Unity
{
    public class MagnetCubeObjectAnnotation : ObjectAnnotation
    {
        public MagnetCube magnetCube;
        public GameHandLandmarkListAnnotation gameHandLandmarkListAnnotation;

        private void Start()
        {
            if (magnetCube == null)
                this.magnetCube = obj.GetComponent<MagnetCube>();
            magnetCube.objectAttractedEvent.AddListener(CheckObject);
            magnetCube.objectLeftEvent.AddListener(SetUnclean);
        }

        private void CheckObject(Collider other)
        {
            SetCleanness(other.GetComponent<Towel>() != null);
        }

        private void SetUnclean() { SetCleanness(false); }
        private void SetCleanness(bool isClean)
        {
            gameHandLandmarkListAnnotation?.SetCleanness(isClean);
        }
    }
}