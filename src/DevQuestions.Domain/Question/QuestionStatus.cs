namespace DevQuestions.Domain.Question;

public enum QuestionStatus
{
    /// <summary>
    /// Вопрос открыт.
    /// </summary>
    OPEN,
    /// <summary>
    /// Вопрос решен.
    /// </summary>
    RESOLVED
}

public static class QuestionStatusExtensions
{
    public static string ToRussian(this QuestionStatus status) => status switch
    {
        QuestionStatus.OPEN => "Открыт",
        QuestionStatus.RESOLVED => "Решен",
        _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
    };
}