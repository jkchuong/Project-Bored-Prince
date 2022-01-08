using System;
using UnityEngine;

namespace Project.Scripts.Parallax
{
    // Allows ParallaxController to move each layer based on parallaxAmount
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(-50f, 50f)]
        [SerializeField]
        [Tooltip("Amount of parallax in the X-direction. 50 simulates being close to the camera, -50 simulates being very far from the camera.")]
        private float xParallaxAmount;
        
        [Range(-50f, 50f)]
        [SerializeField]
        [Tooltip("Amount of parallax in the Y-direction. 50 simulates being close to the camera, -50 simulates being very far from the camera.")]
        private float yParallaxAmount;

        [NonSerialized] private Vector3 newPosition;

        // private bool adjusted = false;

        public void MoveLayer(float positionChangeX, float positionChangeY)
        {
            var transform1 = transform;
            newPosition = transform1.localPosition;
            newPosition.x -= positionChangeX * (-xParallaxAmount) * (Time.deltaTime);
            newPosition.y -= positionChangeY * (-yParallaxAmount) * (Time.deltaTime);
            transform1.localPosition = newPosition;
        }
    }
}
