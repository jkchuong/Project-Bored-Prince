using System;
using UnityEngine;

namespace Project.Scripts.Parallax
{
    // Allows ParallaxController to move each layer based on parallaxAmount
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(-1f, 1f)]
        [SerializeField]
        [Tooltip("Amount of parallax. 1 simulates being close to the camera, -1 simulates being very far from the camera.")]
        private float parallaxAmount;

        [NonSerialized] public Vector3 newPosition;

        private bool adjusted = false;

        public void MoveLayer(float positionChangeX, float positionChangeY)
        {
            newPosition = transform.localPosition;
            newPosition.x -= positionChangeX * (-parallaxAmount * 40) * Time.deltaTime;
            newPosition.y -= positionChangeY * (-parallaxAmount * 40) * Time.deltaTime;
            transform.localPosition = newPosition;
        }
    }
}
