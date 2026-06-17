using DevQuestions.Application.Questions;
using DevQuestions.Domain.Question;

namespace DevQuestions.Infrastructure.Postgres.Repositories;

public class QuestionsRepository : IQuestionsRepository
{
    public Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {


        throw new NotImplementedException();
    }

    public Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Question?> GetByIdAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Question question, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}