namespace DevQuestions.Contracts.Questions.Dtos;

public record QuestionDto(
    Guid Id,
    string Title,
    string Text,
    Guid UserId,
    string? ScreenshotUrl,
    Guid? SolutionID,
    IEnumerable<string> Tags,
    string Status
);
