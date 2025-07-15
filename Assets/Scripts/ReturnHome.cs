using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnHome : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("StartScreen"); 
    }
}
