using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex;
}

public class QuestionLoader : MonoBehaviour
{
    public List<Question> loadedQuestions = new List<Question>();

    void Awake()
    {
        LoadQuestionsFromCSV("lsd_questions.csv");
    }

    void LoadQuestionsFromCSV(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = SplitCSVLine(lines[i]);

                if (parts.Length == 5)
                {
                    Question q = new Question();
                    q.questionText = parts[0];
                    q.answers[0] = parts[1]; // correct
                    q.answers[1] = parts[2];
                    q.answers[2] = parts[3];
                    q.answers[3] = parts[4];

                    ShuffleAnswers(q);
                    loadedQuestions.Add(q);
                }
            }

            Debug.Log("Loaded " + loadedQuestions.Count + " questions.");
        }
        else
        {
            Debug.LogError("CSV file not found at: " + filePath);
        }
    }

    string[] SplitCSVLine(string line)
    {
        List<string> tokens = new List<string>();
        bool insideQuotes = false;
        string currentToken = "";

        foreach (char c in line)
        {
            if (c == '"' && !insideQuotes)
            {
                insideQuotes = true;
            }
            else if (c == '"' && insideQuotes)
            {
                insideQuotes = false;
            }
            else if (c == ',' && !insideQuotes)
            {
                tokens.Add(currentToken);
                currentToken = "";
            }
            else
            {
                currentToken += c;
            }
        }

        tokens.Add(currentToken); // Add the last token
        return tokens.ToArray();
    }

    void ShuffleAnswers(Question q)
    {
        string correctAnswer = q.answers[0];
        System.Random rng = new System.Random();

        for (int i = q.answers.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (q.answers[i], q.answers[j]) = (q.answers[j], q.answers[i]);
        }

        // Update correct index
        for (int i = 0; i < q.answers.Length; i++)
        {
            if (q.answers[i] == correctAnswer)
            {
                q.correctAnswerIndex = i;
                break;
            }
        }
    }
}