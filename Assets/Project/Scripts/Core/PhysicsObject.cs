using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsObject : MonoBehaviour
    {
        [SerializeField] private float minGroundNormalY = 0.65f;
        [SerializeField] private float gravityModifier = 1f;

        protected Vector2 targetVelocity;   
        protected Rigidbody2D rb2d; 
        protected Vector2 velocity;
        protected bool grounded;
    
        private Vector2 groundNormal;
        private ContactFilter2D contactFilter;
        private readonly RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        private readonly List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

        private const float MIN_MOVE_DISTANCE = 0.001f;
        private const float SHELL_RADIUS = 0.01f;

        private void OnEnable()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private protected virtual void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); // Set the contact filter
            contactFilter.useLayerMask = true;
        
            rb2d.gravityScale = 0;
        }

        private void Update()
        {
            targetVelocity = Vector2.zero;
            ComputVelocty();
        }

        protected virtual void ComputVelocty()
        {
        
        }

        private void FixedUpdate()
        {
            velocity += Physics2D.gravity * (gravityModifier * Time.deltaTime);
            velocity.x = targetVelocity.x;

            grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;
        
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Movement(move, false);

            move = Vector2.up * deltaPosition.y;
        
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Movement(move, true);
        }

        private void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > MIN_MOVE_DISTANCE)
            {
                int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + SHELL_RADIUS);
                hitBufferList.Clear();

                for (int i = 0; i < count; i++) 
                {
                    // Lets you jump through platforms from the bottom
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    PlatformEffector2D platform = hitBuffer[i].collider.GetComponent<PlatformEffector2D>(); 
                    if (!platform || (hitBuffer[i].normal == Vector2.up && velocity.y < 0 && yMovement))
                    {
                        hitBufferList.Add(hitBuffer[i]);
                    }
                }
            
                foreach (var raycast in hitBufferList)
                {
                    Vector2 currentNormal = raycast.normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(velocity, currentNormal);
                    if (projection < 0)
                    {
                        velocity -= projection * currentNormal;
                    }

                    float modifiedDistance = raycast.distance - SHELL_RADIUS;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }
        
            rb2d.position += move.normalized * distance;
        }
    }
}
