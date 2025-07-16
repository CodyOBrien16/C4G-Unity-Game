using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public QuestionLoader loader; // Drag this in Inspector
    public TMP_Text questionText;
    public Button[] answerButtons;

    public TMP_Text progressText; // Optional, for showing progress

    private List<Question> questions;
    private int currentIndex = 0;
    private int correctAnswers = 0;
    public FeedbackPopup feedbackPopup; // Drag this in Inspector

    void Start()
    {
        questions = loader.loadedQuestions;
        Debug.Log($"Starting quiz with {questions.Count} questions.");
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentIndex >= questions.Count)
        {
            Debug.Log("Quiz completed!");
            if (correctAnswers >= 3)
            {
                string pathTag = PlayerPrefs.GetString("NextLevelTag", "default");

                switch (pathTag)
                {
                    case "Level 2 Saliva":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2 Salvia");
                        break;
                    case "Level 3 Ecstasy":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3 Ecstasy");
                        break;
                    case "Level 4":
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 4");
                        break;
                    default:
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1 LSD"); // fallback
                        break;
                }
                return;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScreen");
                return;
            }
        }

        Question q = questions[currentIndex];
        Debug.Log("Loading question: " + q.questionText);
        questionText.text = q.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = q.answers[i];

            int selected = i; // capture loop variable
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(selected));
        }
    }

    void OnAnswerSelected(int index)
    {
        Question q = questions[currentIndex];
        if (index == q.correctAnswerIndex)
        {
            correctAnswers++;
            feedbackPopup.ShowMessage("Correct!", true);
            Debug.Log("Correct answer selected!");
        }
        else
        {
            feedbackPopup.ShowMessage("Wrong!", false);
            Debug.Log("Wrong answer selected.");
        }

        progressText.text = $"{correctAnswers}/{currentIndex + 1} correct";
        currentIndex++;
        Invoke("LoadQuestion", 1.5f); // Delay to show result
    }
}