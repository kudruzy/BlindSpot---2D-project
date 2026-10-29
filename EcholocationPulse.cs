using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EcholocationPulse : MonoBehaviour
{
   [Header("Light Reference")]
   [SerializeField] private Light2D playerLight;

   [Header("Visual Ripple Effect")]
   [SerializeField] private GameObject ripplePrefab;

   [Header("Audio Settings")]
   [SerializeField] private AudioSource audioSource; // Drag your Player's AudioSource here
   [SerializeField] private AudioClip pulseSound;

   [Header("Pulse Settings")]
   [SerializeField] private float defaultRadius = 3f;  
   [SerializeField] private float maxPulseRadius = 8f;  
   [SerializeField] private float pulseDuration = 0.5f;
   [SerializeField] private float pulseCooldown = 4f;   

   private bool isPulsing = false;
   private float lastPulseTime = -999f; 

   void Start()
   {
       if (playerLight == null)
           playerLight = GetComponentInChildren<Light2D>();

       if (playerLight != null)
           playerLight.pointLightOuterRadius = defaultRadius;

       if (audioSource == null)
           audioSource = GetComponent<AudioSource>();
   }

   public void TriggerPulse()
   {
       if (!isPulsing && Time.time >= lastPulseTime + pulseCooldown)
       {
           lastPulseTime = Time.time;

           // Play the sonic ping sound!
           if (audioSource != null && pulseSound != null)
           {
               audioSource.PlayOneShot(pulseSound);
           }

           StartCoroutine(PulseRoutine());
       }
   }

   public void ReduceCooldown(float amount)
   {
       pulseCooldown = Mathf.Max(1f, pulseCooldown - amount); 
       Debug.Log("[Echolocation] Apple collected! New cooldown: " + pulseCooldown + " seconds.");
   }

   IEnumerator PulseRoutine()
   {
       isPulsing = true;

       if (ripplePrefab != null)
       {
           Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 0f);
           GameObject ringInstance = Instantiate(ripplePrefab, spawnPos, Quaternion.identity);
           EcholocationRipple rippleScript = ringInstance.GetComponent<EcholocationRipple>();
           if (rippleScript != null)
           {
               rippleScript.StartRipple(pulseDuration, maxPulseRadius);
           }
       }

       float timer = 0f;

       while (timer < pulseDuration)
       {
           timer += Time.deltaTime;
           float progress = timer / pulseDuration;

           playerLight.pointLightOuterRadius = Mathf.Lerp(defaultRadius, maxPulseRadius, progress);
           yield return null;
       }

       timer = 0f;
       float fadeDuration = 0.3f;
       float currentRadius = playerLight.pointLightOuterRadius;

       while (timer < fadeDuration)
       {
           timer += Time.deltaTime;
           float progress = timer / fadeDuration;
           playerLight.pointLightOuterRadius = Mathf.Lerp(currentRadius, defaultRadius, progress);
           yield return null;
       }

       playerLight.pointLightOuterRadius = defaultRadius;
       isPulsing = false;
   }
}