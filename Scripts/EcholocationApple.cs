using UnityEngine;

public class EcholocationApple : MonoBehaviour
{
    [Header("Cooldown Reduction")]
    [SerializeField] private float cooldownReductionAmount = 1f; 

    [Header("Audio Settings")]
    [SerializeField] private AudioClip appleEatSound; // Drag your crunch/bite/pickup sound here

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reduce pulse cooldown
            EcholocationPulse pulseScript = other.GetComponentInChildren<EcholocationPulse>();
            if (pulseScript != null)
            {
                pulseScript.ReduceCooldown(cooldownReductionAmount);
            }

            // Play crunch/eat sound safely before the apple object is destroyed
            if (appleEatSound != null)
            {
                AudioSource.PlayClipAtPoint(appleEatSound, transform.position);
            }

            // Show HUD message
            GameHUD hud = FindFirstObjectByType<GameHUD>();
            if (hud != null)
            {
                hud.ShowBuffMessage("APPLE: -1 second cooldown for pulse!");
            }

            // Destroy the apple
            Destroy(gameObject);
        }
    }
}