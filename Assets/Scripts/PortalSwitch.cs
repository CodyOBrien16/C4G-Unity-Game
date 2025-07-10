using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad = "Arena";
    public string csvFileName = "default.csv"; // Assign this in the Inspector

    public string portalTag = "Level 2";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("SelectedCSV", csvFileName);
            PlayerPrefs.SetString("NextLevelTag", portalTag);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
