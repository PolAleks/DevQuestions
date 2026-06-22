using DevQuestions.Application.Abstractions;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public record GetQuestionsWithFiltersCommand(int PageNumber, int PageSize, string Search, IEnumerable<Guid> TagIds) : ICommand;
