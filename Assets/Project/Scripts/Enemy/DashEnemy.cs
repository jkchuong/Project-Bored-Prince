namespace Project.Scripts.Enemy
{
    public class DashEnemy : LeftRightEnemy
    {
        enum EnemyState
        {
            IdleStay, // Staying still in one spot
            IdleWalk, // Moving left and right default
            Chasing, // Moving towards player
            Attacking, // Doing attack animation
            Searching // Waiting to see if player comes back
        }

        protected override void Update()
        {
            base.Update();
            
        }
        
    }
}
