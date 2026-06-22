using CSharpFunctionalExtensions;
using DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;
using DevQuestions.Domain.Question;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);

    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);

    Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<(IReadOnlyList<Question>, long)> GetQuestionsWithFiltersAsync(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken);

    Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken);
}