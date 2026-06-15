namespace DevQuestions.Domain.Reports;

public enum Status
{
    /// <summary>
    /// Статус открыт
    /// </summary>
    OPEN,
    /// <summary>
    /// Статус в процессе
    /// </summary>
    IN_PROGRESS,
    /// <summary>
    /// Статус решен
    /// </summary>
    RESOLVED,
    /// <summary>
    /// Статус отклонен
    /// </summary>
    DISMISSED
}