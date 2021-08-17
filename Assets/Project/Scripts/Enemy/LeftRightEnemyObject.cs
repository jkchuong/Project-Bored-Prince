using UnityEngine;

namespace Project.Scripts.Enemy
{
    public class LeftRightEnemyObject : EnemyObject
    {
        [SerializeField] private float maxSpeed;

        [Header("Ground and Wall Detection")]
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float groundRaycastLength = 0.5f;
        [SerializeField] private Vector2 rightRaycastOffset;
        [SerializeField] private Vector2 leftRaycastOffset;
        [SerializeField] private float wallRaycastLength = 0.5f;
    
        private RaycastHit2D rightLedgeRaycastHit;
        private RaycastHit2D leftLedgeRaycastHit;
    
        private RaycastHit2D rightWallRaycastHit;
        private RaycastHit2D leftWallRaycastHit;

        private int direction = 1;

        private void Update()
        {
            targetVelocity = new Vector2(maxSpeed * direction, 0);
            CheckLedgesAndWalls();
        }

        private void CheckLedgesAndWalls()
        {
            Vector2 position = transform.position;

            rightLedgeRaycastHit = Physics2D.Raycast(position + rightRaycastOffset, Vector2.down,
                groundRaycastLength, groundMask);

            rightWallRaycastHit = Physics2D.Raycast(position, Vector2.right, wallRaycastLength, groundMask);

            if (!rightLedgeRaycastHit.collider || rightWallRaycastHit.collider)
            {
                direction = -1;
            }

            leftLedgeRaycastHit = Physics2D.Raycast(position + leftRaycastOffset, Vector2.down,
                groundRaycastLength, groundMask);
        
            leftWallRaycastHit = Physics2D.Raycast(position, Vector2.left, wallRaycastLength, groundMask);

            if (!leftLedgeRaycastHit.collider || leftWallRaycastHit.collider)
            {
                direction = 1;
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Vector2 position = transform.position;

            Gizmos.color = Color.green;
            Gizmos.DrawRay(position + rightRaycastOffset, Vector2.down * groundRaycastLength);
            Gizmos.DrawRay(position + leftRaycastOffset, Vector2.down * groundRaycastLength);
            Gizmos.DrawRay(position, Vector2.right * wallRaycastLength);
            Gizmos.DrawRay(position, Vector2.left * wallRaycastLength);
        }
    
        [ContextMenu("Set Left and Right Edges")]
        private void ResetEdges()
        {
            Vector3 enemySize = GetComponent<CapsuleCollider2D>().bounds.size;
            rightRaycastOffset = new Vector2(enemySize.x / 2, -enemySize.y / 2);
            leftRaycastOffset = new Vector2(-enemySize.x / 2, -enemySize.y / 2);
        }
    
#endif
    }
}
