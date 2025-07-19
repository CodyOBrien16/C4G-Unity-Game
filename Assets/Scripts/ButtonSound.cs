using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundAttacher : MonoBehaviour
{
    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() =>
            {
                if (UIAudio.Instance != null)
                    UIAudio.Instance.PlayClick();
            });
        }
    }
}
