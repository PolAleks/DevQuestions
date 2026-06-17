using DevQuestions.Application.Questions;
using DevQuestions.Domain.Question;

namespace DevQuestions.Infrastructure.Postgres.Repositories;

public class QuestionsEfCoreRepository : IQuestionsRepository
{
    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Question?> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}