using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   [Header("UI References")]
   [SerializeField] private TextMeshProUGUI roundText;
   [SerializeField] private GameObject winPanel; 

   [Header("Audio Settings")]
   [SerializeField] private AudioClip winJingle; // Drag your victory jingle audio file here

   private int currentRound = 1;

   private void Awake()
   {
       if (Instance == null)
       {
           Instance = this;
           DontDestroyOnLoad(gameObject);
       }
       else
       {
           Destroy(gameObject);
           return;
       }
   }

   private void Start()
   {
       UpdateRoundUI();
       if (winPanel != null)
       {
           winPanel.SetActive(false);
       }
   }

   // This is called by the ExitDoor script when the player wins the level
   public void OnPlayerReachedExit()
   {
       // Play victory jingle (PlayClipAtPoint works even when Time.timeScale = 0)
       if (winJingle != null)
       {
           AudioSource.PlayClipAtPoint(winJingle, Camera.main.transform.position);
       }

       if (winPanel != null)
       {
           winPanel.SetActive(true);
       }

       Time.timeScale = 0f;
   }

   public void ContinueToNextLevel()
   {
       Time.timeScale = 1f; 
       AdvanceRound();
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void AdvanceRound()
   {
       currentRound++;
       UpdateRoundUI();
   }

   private void UpdateRoundUI()
   {
       if (roundText != null)
       {
           roundText.text = "Round: " + currentRound;
       }
   }
}