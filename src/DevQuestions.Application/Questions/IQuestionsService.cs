using CSharpFunctionalExtensions;
using DevQuestions.Contracts.Questions.Dtos;
using Shared;

namespace DevQuestions.Application.Questions;

public interface IQuestionsService
{
    /// <summary>
    /// Метод создания вопроса
    /// </summary>
    /// <param name="questionDto">DTO для создания вопроса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат работы метода, либо ID созданного вопроса, либо список ошибок.</returns>
    Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken);

    /// <summary>
    /// Метод добавления ответа на вопрос
    /// </summary>
    /// <param name="questionId">ID вопроса</param>
    /// <param name="answerDto">DTO для добавления ответа на вопрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат работы метода, либо ID созданного ответа, либо список ошибок</returns>
    Task<Result<Guid, Failure>> AddAnswer(Guid questionId, AddAnswerDto answerDto, CancellationToken cancellationToken);
}
