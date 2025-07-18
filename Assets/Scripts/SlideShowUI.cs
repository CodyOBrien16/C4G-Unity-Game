using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideShowUI : MonoBehaviour
{
    [System.Serializable]
    public class Slide
    {
        public Sprite image;
        [TextArea]
        public string text;
    }

    public Slide[] slides;
    public Image slideImage;
    public TextMeshProUGUI slideText;
    public Button leftButton;
    public Button rightButton;

    private int currentIndex = 0;

    void Start()
    {
        leftButton.onClick.AddListener(PreviousSlide);
        rightButton.onClick.AddListener(NextSlide);
        UpdateSlide();
    }


    void PreviousSlide()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateSlide();
        }
    }

    void NextSlide()
    {
        if (currentIndex < slides.Length - 1)
        {
            currentIndex++;
            UpdateSlide();
        }
    }

    void UpdateSlide()
    {
        if (slides.Length == 0) return;

        slideImage.sprite = slides[currentIndex].image;
        slideText.text = slides[currentIndex].text;

        leftButton.interactable = currentIndex > 0;
        rightButton.interactable = currentIndex < slides.Length - 1;
    }
}
