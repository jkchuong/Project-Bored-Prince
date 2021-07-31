using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : PhysicsObject
{
    private void Update()
    {
        targetVelocity = new Vector2(1, 0);
    }
}
