using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
   [Header("Movement Settings")]
   [SerializeField] private float moveSpeed = 5f;


   private Rigidbody2D rb;
   private Vector2 movementInput;
   private Vector2 lastFacingDir = Vector2.up; // Default facing up for projectile firing


   public Vector2 LastFacingDir => lastFacingDir;


   private void Awake()
   {
       rb = GetComponent<Rigidbody2D>();
   }


   private void Start()
   {
       // Start player inside the open area of the maze
       transform.position = new Vector3(2f, 2f, 0f);
   }


   private void Update()
   {
       float x = 0f;
       float y = 0f;


       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) y += 1f;
       if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) y -= 1f;
       if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) x += 1f;
       if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) x -= 1f;


       movementInput = new Vector2(x, y).normalized;


       if (movementInput != Vector2.zero)
       {
           lastFacingDir = movementInput;
       }
   }


   private void FixedUpdate()
   {
       rb.linearVelocity = movementInput * moveSpeed;
   }
}

