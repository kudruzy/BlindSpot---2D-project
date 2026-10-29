using UnityEngine;


public class EcholocationGun : MonoBehaviour
{
   [SerializeField] private float fireCooldown = 0.5f;
   private float nextFireTime = 0f;


   private EcholocationPulse echolocationPulse;


   void Awake()
   {
       echolocationPulse = GetComponent<EcholocationPulse>();
       if (echolocationPulse == null)
       {
           echolocationPulse = GetComponentInParent<EcholocationPulse>();
       }
   }


   private void Update()
   {
       // Fire echolocation pulse on Left Mouse Click or Spacebar using cooldown
       if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && Time.time >= nextFireTime)
       {
           FirePulse();
           nextFireTime = Time.time + fireCooldown;
       }
   }


   private void FirePulse()
   {
       if (echolocationPulse != null)
       {
           echolocationPulse.TriggerPulse();
       }
       else
       {
           Debug.LogWarning("[Echolocation] EcholocationPulse component not found on Player!");
       }
   }
}

