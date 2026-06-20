using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using DevQuestions.Application.Database;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Question;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _createQuestionDtoValidator;
    private readonly IValidator<AddAnswerDto> _addAnswerDtoValidator;
    private readonly ITransactionManager _transactionManager;
    private readonly IUserCommunicationService _userCommunicationService;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(IQuestionsRepository questionsRepository,
                            IValidator<CreateQuestionDto> createQuestionDtoValidator,
                            IValidator<AddAnswerDto> addAnswerDtoValidator,
                            ITransactionManager transactionManager,
                            IUserCommunicationService userCommunicationService,
                            ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _createQuestionDtoValidator = createQuestionDtoValidator;
        _addAnswerDtoValidator = addAnswerDtoValidator;
        _transactionManager = transactionManager;
        _userCommunicationService = userCommunicationService;
        _logger = logger;
    }


    public async Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _createQuestionDtoValidator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // Валидация бизнес логики
        int openUserQuestionsCount = await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            return Errors.Questions.ToManyQuestions().ToFailure();
        }

        // Создание сущности Question
        var questionId = Guid.NewGuid();

        var question = new Question()
        {
            Id = questionId,
            Title = questionDto.Title,
            Text = questionDto.Text,
            Tags = questionDto.TagIds.ToList(),
            UserId = questionDto.UserId
        };

        // Сохранение сущности Question в БД
        await _questionsRepository.AddAsync(question, cancellationToken);

        // Логирование успеха или неуспеха сохранения
        _logger.LogInformation("Question created with id {questionId} successfully", questionId);

        return questionId;
    }

    public async Task<Result<Guid, Failure>> AddAnswer(Guid questionId, AddAnswerDto answerDto, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _addAnswerDtoValidator.ValidateAsync(answerDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // Проверка рейтинга пользователя
        var userRatingResult = await _userCommunicationService.GetUserRatingAsync(answerDto.UserId, cancellationToken);
        if (userRatingResult.IsFailure)
        {
            return userRatingResult.Error;
        }

        if(userRatingResult.Value <= 0)
        {
            return Errors.Questions.LowRating().ToFailure();
        }

        var transaction = await _transactionManager.BeginTransactionAsync(cancellationToken);

        var questionResult = await _questionsRepository.GetByIdAsync(questionId, cancellationToken);
        if (questionResult.IsFailure)
        {
            return questionResult.Error;
        }

        var question = questionResult.Value;

        var answer = new Answer(Guid.NewGuid(), answerDto.UserId, answerDto.Text, questionId);

        question.Answers.Add(answer);

        await _questionsRepository.SaveAsync(question, cancellationToken);

        transaction.Commit();

        _logger.LogInformation("Answer added to question with id {questionId} successfully", questionId);

        return answer.Id;
    }
}