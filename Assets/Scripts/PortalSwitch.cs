using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneToLoad = "Arena";
    public string csvFileName = "default.csv"; // Assign this in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("SelectedCSV", csvFileName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
