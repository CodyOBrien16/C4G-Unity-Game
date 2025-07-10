using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public QuestionLoader loader; // Drag this in Inspector
    public TMP_Text questionText;
    public Button[] answerButtons;

    private List<Question> questions;
    private int currentIndex = 0;

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
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
            return;
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
        Debug.Log(index == q.correctAnswerIndex ? "Correct!" : "Incorrect!");

        currentIndex++;
        Invoke("LoadQuestion", 1.5f); // Delay to show result
    }
}