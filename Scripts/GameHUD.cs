using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI buffText;

    private void Start()
    {
        if (buffText != null)
        {
            buffText.text = ""; // Start blank
        }
    }

    // Called when an apple is collected
    public void ShowBuffMessage(string message)
    {
        if (buffText != null)
        {
            buffText.text = message;
        }
    }
}