using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "Arena" || scene.name == "WinScreen" || scene.name == "EndScreen")
        {
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
       }
    }
}
