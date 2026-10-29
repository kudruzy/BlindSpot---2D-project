using UnityEngine;


public class ExitDoor : MonoBehaviour
{
   private bool hasTriggered = false;


   private void OnTriggerEnter2D(Collider2D other)
   {
       if (hasTriggered) return;


       if (other.CompareTag("Player"))
       {
           hasTriggered = true;
           if (GameManager.Instance != null)
           {
               GameManager.Instance.OnPlayerReachedExit();
           }
       }
   }


   // Reset state if regenerated dynamically
   public void ResetDoor()
   {
       hasTriggered = false;
   }
}

