using UnityEngine;

public class UIAudio : MonoBehaviour
{
    public static UIAudio Instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClick()
    {
        if (audioSource != null)
            audioSource.PlayOneShot(audioSource.clip);
    }
}
