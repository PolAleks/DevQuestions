using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Contracts.Questions.Responses;

public record QuestionsResponse(IEnumerable<QuestionDto> Questions, int TotalCount);
