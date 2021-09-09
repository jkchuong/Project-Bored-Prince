using System.Collections;
using UnityEngine;

namespace Project.Scripts.UI
{
    // TODO: Play routes in LevelSelectorUI
    public class CharBezierFollow : MonoBehaviour
    {
        [SerializeField] private float speedModifier;
        [SerializeField] private Transform[] routes;
        [SerializeField] private Transform[] points;

        private float tParam;

        private Vector2 characterPosition;

        public bool coroutineAllowed;


        private void Start()
        {
            tParam = 0f;
            speedModifier = 0.5f;
            coroutineAllowed = true;
        }
 
        private IEnumerator GoByTheRoute(int routeNumber)
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNumber].GetChild(0).position;
            Vector2 p1 = routes[routeNumber].GetChild(1).position;
            Vector2 p2 = routes[routeNumber].GetChild(2).position;
            Vector2 p3 = routes[routeNumber].GetChild(3).position;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;

                characterPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                                    3 * Mathf.Pow(tParam, 2) * (1 - tParam) * p2 +
                                    Mathf.Pow(tParam, 3) * p3;

                transform.position = characterPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            coroutineAllowed = true;
        }

        private IEnumerator GoByTheRouteReverse(int routeNumber)
        {
            coroutineAllowed = false;

            Vector2 p0 = routes[routeNumber].GetChild(3).position;
            Vector2 p1 = routes[routeNumber].GetChild(2).position;
            Vector2 p2 = routes[routeNumber].GetChild(1).position;
            Vector2 p3 = routes[routeNumber].GetChild(0).position;

            while (tParam < 1)
            {
                tParam += Time.deltaTime * speedModifier;

                characterPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                                    3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                                    3 * Mathf.Pow(tParam, 2) * (1 - tParam) * p2 +
                                    Mathf.Pow(tParam, 3) * p3;

                transform.position = characterPosition;
                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            coroutineAllowed = true;
        }
        
        public void MoveOnRoute(int routeToGo)
        {
            if (coroutineAllowed)
                StartCoroutine(GoByTheRoute(routeToGo));
        }
        
        public void MoveOnRouteReverse(int routeToGo)
        {
            if (coroutineAllowed)
                StartCoroutine(GoByTheRouteReverse(routeToGo));
        }

        public void SetPosition(int positionNumber)
        {
            transform.position = points[positionNumber].position;
        }
    }
}
