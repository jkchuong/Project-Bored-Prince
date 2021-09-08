using UnityEngine;

namespace Project.Scripts.UI
{
    public class Route : MonoBehaviour
    {
        [SerializeField] private float sphereRadius = 0.2f;
        [SerializeField] private Transform[] controlPoints;

        private Vector2 gizmosPosition;

        private void OnDrawGizmos()
        {
            for (float t = 0; t <= 1; t += 0.02f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                                 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                                 3 * Mathf.Pow(t, 2) * (1 - t) * controlPoints[2].position +
                                 Mathf.Pow(t, 3) * controlPoints[3].position;

                Gizmos.DrawSphere(gizmosPosition, sphereRadius);
            }
            
            Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y),
                new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
            
            Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y),
                new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
        }
    }
}
