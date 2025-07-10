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
        LoadCSV("lsd_questions.csv");
    }

    void LoadCSV(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogError("CSV not found: " + path);
            return;
        }

        string[] lines = File.ReadAllLines(path);

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            string[] cols = SplitCSVLine(lines[i]);
            if (cols.Length == 5)
            {
                Question q = new Question();
                q.questionText = cols[0];
                q.answers[0] = cols[1]; // correct
                q.answers[1] = cols[2];
                q.answers[2] = cols[3];
                q.answers[3] = cols[4];

                ShuffleAnswers(q);
                loadedQuestions.Add(q);
            }
        }

        Debug.Log($"Loaded {loadedQuestions.Count} questions!");
    }

    string[] SplitCSVLine(string line)
    {
        List<string> parts = new List<string>();
        bool insideQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"' && !insideQuotes) insideQuotes = true;
            else if (c == '"' && insideQuotes) insideQuotes = false;
            else if (c == ',' && !insideQuotes)
            {
                parts.Add(current);
                current = "";
            }
            else current += c;
        }

        parts.Add(current);
        return parts.ToArray();
    }

    void ShuffleAnswers(Question q)
    {
        string correct = q.answers[0];
        System.Random rng = new System.Random();
        for (int i = 0; i < q.answers.Length; i++)
        {
            int j = rng.Next(i, q.answers.Length);
            (q.answers[i], q.answers[j]) = (q.answers[j], q.answers[i]);
        }

        for (int i = 0; i < q.answers.Length; i++)
            if (q.answers[i] == correct)
                q.correctAnswerIndex = i;
    }
}