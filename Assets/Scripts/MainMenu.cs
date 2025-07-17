using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Level 1 LSD");
    }
    public void Instructions()
    {
        SceneManager.LoadSceneAsync("Instructions");
    }

    public void Return()
    {
        SceneManager.LoadSceneAsync("StartScreen");
    }
}
