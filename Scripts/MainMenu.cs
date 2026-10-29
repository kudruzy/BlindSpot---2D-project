using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject startScreenPanel;
    [SerializeField] private TextMeshProUGUI objectiveText; // <--- Add this line

    private void Awake()
    {
        Time.timeScale = 0f;

        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(true);
        }

        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(false); // <--- Add this line
        }
    }

    public void StartGame()
    {
        if (startScreenPanel != null)
        {
            startScreenPanel.SetActive(false);
        }

        if (objectiveText != null) // <--- Add this block
        {
            objectiveText.gameObject.SetActive(true);
            objectiveText.text = "Reach the green door before the zombies get you!";
        }

        Time.timeScale = 1f;
    }
}