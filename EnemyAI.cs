using UnityEngine;


public class EnemyAI : MonoBehaviour
{
   [Header("Movement Settings")]
   [SerializeField] private float moveSpeed = 1.5f;


   private Transform playerTransform;
   private Rigidbody2D rb;


   private void Start()
   {
       rb = GetComponent<Rigidbody2D>();


       GameObject player = GameObject.FindGameObjectWithTag("Player");
       if (player != null)
       {
           playerTransform = player.transform;
       }
   }


   private void FixedUpdate()
   {
       if (playerTransform == null || rb == null) return;


       // Move towards player; physics engine handles solid wall collisions automatically
       Vector2 direction = ((Vector2)playerTransform.position - rb.position).normalized;
       rb.linearVelocity = direction * moveSpeed;
   }


   public void DeleteEnemy()
   {
       Destroy(gameObject);
   }


   // Handles damage via the second collider (set to Is Trigger = true)
   private void OnTriggerStay2D(Collider2D collision)
   {
       if (collision.CompareTag("Player"))
       {
           PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
           if (playerHealth != null)
           {
               playerHealth.TakeDamage(15f * Time.deltaTime);
           }
       }
   }
}

