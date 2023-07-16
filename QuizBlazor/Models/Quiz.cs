#pragma warning disable CS8618

using System.Text;

namespace QuizBlazor.Models;

public class Quiz
{
    public string Question { get; set; }
    public List<Answer> Answers { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.AppendLine($"Question: {Question}");
        sb.AppendLine("Answers:");
        foreach (Answer answer in Answers)
        {
            sb.AppendLine(answer.ToString());
        }
        return sb.ToString();
    }
}

public class Answer
{
    public string Name { get; set; }
    public bool IsCorrect { get; set; }

    public override string ToString()
    {
        return $"{Name} (IsCorrect: {IsCorrect})";
    }
}