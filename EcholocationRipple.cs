using System.Collections;
using UnityEngine;


public class EcholocationRipple : MonoBehaviour
{
   [Header("Ripple Settings")]
   [SerializeField] private float maxScale = 16f;       
   [SerializeField] private float expansionDuration = 0.5f;


   [Header("Wall Reveal Settings")]
   [SerializeField] private Color revealedColor = new Color(1f, 0.4f, 0.7f); // Adjust this to your exact pink in the Inspector!


   private SpriteRenderer spriteRenderer;
   private CircleCollider2D rippleCollider;
   private Rigidbody2D rb;


   void Awake()
   {
       spriteRenderer = GetComponent<SpriteRenderer>();
      
       // Ensure CircleCollider2D exists and is set to Trigger
       rippleCollider = GetComponent<CircleCollider2D>();
       if (rippleCollider == null)
       {
           rippleCollider = gameObject.AddComponent<CircleCollider2D>();
       }
       rippleCollider.isTrigger = true;


       // Ensure Rigidbody2D exists so 2D physics triggers fire
       rb = GetComponent<Rigidbody2D>();
       if (rb == null)
       {
           rb = gameObject.AddComponent<Rigidbody2D>();
       }
       rb.bodyType = RigidbodyType2D.Kinematic;
   }


   public void StartRipple(float duration, float targetRadius)
   {
       expansionDuration = duration;
       maxScale = targetRadius * 2f;
       StartCoroutine(ExpandAndFade());
   }


   private IEnumerator ExpandAndFade()
   {
       Vector3 initialScale = Vector3.zero;
       Vector3 targetScale = new Vector3(maxScale, maxScale, 1f);


       Color initialColor = spriteRenderer.color;
       float timer = 0f;


       while (timer < expansionDuration)
       {
           timer += Time.deltaTime;
           float progress = timer / expansionDuration;


           // Expand ring scale
           transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);


           // Fade out the visual ring sprite itself
           Color newColor = initialColor;
           newColor.a = Mathf.Lerp(initialColor.a, 0f, progress);
           spriteRenderer.color = newColor;


           yield return null;
       }


       Destroy(gameObject);
   }


   private void OnTriggerEnter2D(Collider2D other)
   {
       ProcessHit(other);
   }


   private void OnTriggerStay2D(Collider2D other)
   {
       ProcessHit(other);
   }


   private void ProcessHit(Collider2D other)
   {
       // Ignore player and the ripple itself
       if (other.CompareTag("Player") || other.gameObject == gameObject) return;


       // 1. Destroy Enemies on contact
       if (other.CompareTag("Enemy"))
       {
           Debug.Log("[Echolocation] Pulse eliminated an enemy!");
           Destroy(other.gameObject);
           return;
       }


       // 2. Turn black walls into your chosen pink color permanently when swept over
       SpriteRenderer tileRenderer = other.GetComponent<SpriteRenderer>();
       if (tileRenderer != null)
       {
           tileRenderer.enabled = true;
           tileRenderer.color = revealedColor; // Swaps black to your pink color forever
       }
   }
}



