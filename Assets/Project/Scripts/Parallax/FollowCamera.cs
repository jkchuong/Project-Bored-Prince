using System;
using UnityEngine;

namespace Project.Scripts.Parallax
{
    public class FollowCamera : MonoBehaviour
    {
        private Transform mainCamera;

        private void Start()
        {
            mainCamera = Camera.main.transform;
        }

        private void Update()
        {
            transform.position = (Vector2)mainCamera.position;
        }
    }
}
