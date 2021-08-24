using Project.Scripts.Character;
using UnityEngine;

namespace Project.Scripts
{
    public class Checkpoint : MonoBehaviour
    {
        private Collider2D c2d;
    
        private void Awake()
        {
            c2d = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Checkpoint = transform.position;
            }

            c2d.enabled = false;
        }
    }
}
