using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class FeedbackPopup : MonoBehaviour
{
    public TextMeshProUGUI feedbackText;
    public Image backgroundImage;
    public GameObject feedbackPanel;

    void Start()
    {
        feedbackPanel.SetActive(false); // Hides the panel at start
    }

    public void ShowMessage(string message, bool isCorrect)
    {
        feedbackText.text = message;
        backgroundImage.color = isCorrect ? Color.green : Color.red;
        feedbackPanel.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        feedbackPanel.SetActive(false);
    }
}