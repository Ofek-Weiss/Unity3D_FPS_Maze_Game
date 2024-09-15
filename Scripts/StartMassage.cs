using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Use this for standard UI Text
// using TMPro; // Uncomment this line if using TextMeshPro

public class StartMessage : MonoBehaviour
{
    public GameObject startMessage; // Drag your Text component here in the inspector
    // public TextMeshProUGUI startMessage; // Use this line instead if using TextMeshPro
    private static bool hasShown = false; // Static variable to track if the message has been shown
    public GameObject StartPanel;
    public GameObject EndPanel;

    // Start method used as a coroutine
    IEnumerator Start()
    {
        if (!hasShown)
        {
            yield return new WaitForSeconds(3); // Wait for 3 seconds before showing the message
            startMessage.SetActive(true); // Activate the message
            StartCoroutine(HideTextAfterDelay(8)); // Hides the text after 8 seconds
            hasShown = true; // Set to true so it doesn't show again
        }
        else
        {
            startMessage.SetActive(false); // Keep it disabled if already shown
            StartPanel.SetActive(false);
            yield return new WaitForSeconds(3); // Wait for 3 seconds before showing the end panel
            EndPanel.SetActive(true);
        }
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        startMessage.SetActive(false); // Disable the text element
    }
}