using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : PhysicsObject
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    private protected override void Start()
    {
        base.Start();
        rb2d.gravityScale = 0;
    }

    private void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpSpeed;
        }
        
    }
}
