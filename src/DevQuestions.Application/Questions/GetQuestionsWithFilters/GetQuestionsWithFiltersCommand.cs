using DevQuestions.Application.Abstractions;

namespace DevQuestions.Application.Questions.GetQuestionsWithFilters;

public record GetQuestionsWithFiltersCommand(int PageNumber, int PageSize, string Search, IEnumerable<Guid> TagIds) : ICommand;
