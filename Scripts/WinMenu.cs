using UnityEngine;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{
   [Header("UI Panels")]
   [SerializeField] private GameObject winPanel;
   [SerializeField] private GameObject startScreenPanel;


   // Hook this up to your "Continue" / "Next Level" button
   public void OnContinueClicked()
   {
       // 1. Hide the win screen
       if (winPanel != null)
       {
           winPanel.SetActive(false);
       }


       // 2. Advance the round counter
       if (GameManager.Instance != null)
       {
           GameManager.Instance.AdvanceRound();
       }


       // 3. Generate a fresh maze using FindAnyObjectByType for Unity 6 compatibility
       MazeGenerator mazeGen = FindAnyObjectByType<MazeGenerator>();
       if (mazeGen != null)
       {
           mazeGen.GenerateMaze();
       }


       // 4. Unfreeze time so gameplay continues
       Time.timeScale = 1f;
   }


   // Hook this up to your "Quit" / "Quit Run" button
   public void OnQuitClicked()
   {
       // Reload the scene to reset the run and return to the start menu
       Time.timeScale = 0f;
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}

