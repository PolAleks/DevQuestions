using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Question;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerHandler : ICommandHandler<Guid, AddAnswerCommand>
{
    private readonly IValidator<AddAnswerDto> _validator;
    // private readonly IUserCommunicationService _userCommunicationService;
    private readonly IQuestionsRepository _repository;
    // private readonly ITransactionManager _transactionManager;
    private readonly ILogger<AddAnswerHandler> _logger;

    public AddAnswerHandler(IValidator<AddAnswerDto> addAnswerDtoValidator,
                            // IUserCommunicationService userCommunicationService,
                            IQuestionsRepository repository,
                            // ITransactionManager transactionManager,
                            ILogger<AddAnswerHandler> logger)
    {
        _validator = addAnswerDtoValidator;
        // _userCommunicationService = userCommunicationService;
        _repository = repository;
        // _transactionManager = transactionManager;
        _logger = logger;
    }

    public async Task<Result<Guid, Failure>> Handle(AddAnswerCommand command, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(command.AnswerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // Проверка рейтинга пользователя
        // var userRatingResult = await _userCommunicationService.GetUserRatingAsync(command.AnswerDto.UserId, cancellationToken);
        // if (userRatingResult.IsFailure)
        // {
        //     return userRatingResult.Error;
        // }

        // if (userRatingResult.Value <= 0)
        // {
        //     return Errors.Questions.LowRating().ToFailure();
        // }

        // var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _repository.GetByIdAsync(command.QuestionId, cancellationToken);
        if (questionResult.IsFailure)
        {
            return questionResult.Error;
        }

        var question = questionResult.Value;

        var answer = new Answer(Guid.NewGuid(), command.AnswerDto.UserId, command.AnswerDto.Text, command.QuestionId);

        question.Answers.Add(answer);

        await _repository.SaveAsync(question, cancellationToken);

        // transaction.Commit();

        _logger.LogInformation("Answer added to question with id {questionId} successfully", command.QuestionId);

        return answer.Id;
    }
}