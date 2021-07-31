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
        private Vector3 mainCameraPosition;
        
        private void Start()
        {
            onCameraMove += MoveLayer;
            FindLayer();
            
            mainCameraPosition = Camera.main.transform.position;
            oldCameraPosition.x = mainCameraPosition.x;
            oldCameraPosition.y = mainCameraPosition.y;
        }

        private void FixedUpdate()
        {
            if (Math.Abs(mainCameraPosition.x - oldCameraPosition.x) > 0 || Math.Abs((mainCameraPosition.y) - oldCameraPosition.y) > 0)
            {
                if (onCameraMove != null)
                {
                    var cameraPositionChange = new Vector2(oldCameraPosition.x - mainCameraPosition.x, oldCameraPosition.y - mainCameraPosition.y);
                    onCameraMove(cameraPositionChange.x, cameraPositionChange.y);
                }

                oldCameraPosition = new Vector2(mainCameraPosition.x, mainCameraPosition.y);
            }
        }

        private void FindLayer()
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

        private void MoveLayer(float positionChangeX, float positionChangeY)
        {
            foreach (ParallaxLayer layer in parallaxLayers)
            {
                layer.MoveLayer(positionChangeX, positionChangeY);
            }
        }
    }
}
