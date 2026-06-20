using CSharpFunctionalExtensions;
using Dapper;
using DevQuestions.Application.Database;
using DevQuestions.Application.Questions;
using DevQuestions.Domain.Question;
using Shared;

namespace DevQuestions.Infrastructure.Postgres.Repositories;

public class QuestionsSqlRepository : IQuestionsRepository
{
    private readonly ISqlConnection _connectionFactory;

    public QuestionsSqlRepository(ISqlConnection connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Guid> AddAsync(Question question, CancellationToken cancellationToken)
    {
        const string sql = """
                           INSERT INTO Questions (id, title, text, user_id, screenshot_id, tags, status)
                           VALUES (@Id, @Title, @Text, @UserId, @ScreenshotId, @Tags, @Status);
                           """;

        using var connection = _connectionFactory.GetConnection();

        await connection.ExecuteAsync(sql, new
        {
            Id = question.Id,
            Title = question.Title,
            Text = question.Text,
            UserId = question.UserId,
            ScreenshotId = question.ScreenshotId,
            Tags = question.Tags.ToArray(),
            Status = question.Status
        });
        
        return question.Id;
    }

    public async Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Question, Failure>> GetByIdAsync(Guid questionId, CancellationToken cancellationToken) 
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