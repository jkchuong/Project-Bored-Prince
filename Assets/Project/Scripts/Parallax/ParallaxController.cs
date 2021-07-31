using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Parallax
{
    // Finds all game objects that have ParallaxLayer and moves them
    public class ParallaxController : MonoBehaviour
    {
        private delegate void ParallaxCameraDelegate(float cameraPositionChangeX, float cameraPositionChangeY);

        private ParallaxCameraDelegate onCameraMove;
        private Vector2 oldCameraPosition;
        private readonly List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

        private Camera mainCamera;

        private void Start()
        {
            onCameraMove += MoveLayer;
            FindLayers();
            mainCamera = Camera.main;
            
            Vector3 cameraPos = mainCamera.transform.position;
            oldCameraPosition.x = cameraPos.x;
            oldCameraPosition.y = cameraPos.y;
        }

        private void FixedUpdate()
        {
            Vector3 cameraPos = mainCamera.transform.position;
            
            if (Math.Abs(cameraPos.x - oldCameraPosition.x) > 0.01 || Math.Abs((cameraPos.y) - oldCameraPosition.y) > 0.01)
            {
                if (onCameraMove != null)
                {
                    var cameraPositionChange = new Vector2(oldCameraPosition.x - cameraPos.x, oldCameraPosition.y - cameraPos.y);
                    onCameraMove(cameraPositionChange.x, cameraPositionChange.y);
                }

                oldCameraPosition = new Vector2(cameraPos.x, cameraPos.y);
            }
        }

        //Finds all the objects that have a ParallaxLayer component, and adds them to the parallaxLayers list.
        private void FindLayers()
        {
            parallaxLayers.Clear();

            for (int i = 0; i < transform.childCount; i++)
            {
                ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

                if (layer != null)
                {
                    parallaxLayers.Add(layer);
                }
            }
        }

        //Move each layer based on each layers position. This is being used via the ParallaxLayer script
        private void MoveLayer(float positionChangeX, float positionChangeY)
        {
            foreach (ParallaxLayer layer in parallaxLayers)
            {
                layer.MoveLayer(positionChangeX, positionChangeY);
            }
        }
    }
}
