using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [Header("Potion Settings")]
    [SerializeField] private float healAmount = 15f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Heal the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
            }

            // Optional: Show notification on HUD
            GameHUD hud = FindFirstObjectByType<GameHUD>();
            if (hud != null)
            {
                hud.ShowBuffMessage("POTION: +15 HP Restored!");
            }

            // Destroy the potion after pickup
            Destroy(gameObject);
        }
    }
}