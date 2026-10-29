using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
   [Header("Health Settings")]
   [SerializeField] private float maxHealth = 100f;
   private float currentHealth;

   [Header("Damage Cooldown")]
   [SerializeField] private float invulnerabilityDuration = 1f; // Time in seconds between allowed hits
   private float lastDamageTime = -999f;

   [Header("UI Reference")]
   [SerializeField] private Slider healthSlider;
   [SerializeField] private GameObject gameOverPanel; 

   [Header("Audio Settings")]
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip ouchSound;

   private bool isDead = false;

   private void Start()
   {
       currentHealth = maxHealth;
       Time.timeScale = 1f; 

       if (healthSlider != null)
       {
           healthSlider.maxValue = maxHealth;
           healthSlider.value = currentHealth;
       }

       if (gameOverPanel != null)
       {
           gameOverPanel.SetActive(false); 
       }

       if (audioSource == null)
           audioSource = GetComponent<AudioSource>();
   }

   public void TakeDamage(float amount)
   {
       // If dead, or if we were hit recently, ignore this damage request
       if (isDead || Time.time < lastDamageTime + invulnerabilityDuration) return;

       lastDamageTime = Time.time; // Reset the cooldown timer
       currentHealth -= amount;
       currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

       // Play the "ouch" sound once per hit!
       if (audioSource != null && ouchSound != null)
       {
           audioSource.PlayOneShot(ouchSound);
       }

       if (healthSlider != null)
       {
           healthSlider.value = currentHealth;
       }

       if (currentHealth <= 0)
       {
           Die();
       }
   }

   public void Heal(float amount)
   {
       if (isDead) return;

       currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

       if (healthSlider != null)
       {
           healthSlider.value = currentHealth;
       }
   }

   private void Die()
   {
       isDead = true;

       if (gameOverPanel != null)
       {
           gameOverPanel.SetActive(true);
       }

       Time.timeScale = 0f;
   }

   public void RestartGame()
   {
       Time.timeScale = 1f; 
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}