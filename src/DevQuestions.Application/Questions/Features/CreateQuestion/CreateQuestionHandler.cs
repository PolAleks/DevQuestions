using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.Extensions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Domain.Question;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions.Features.CreateQuestion;

public class CreateQuestionHandler : ICommandHandler<Guid, CreateQuestionCommand>
{
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly IQuestionsRepository _repository;
    private readonly ILogger<QuestionsService> _logger;


    public CreateQuestionHandler(
        IValidator<CreateQuestionDto> createQuestionDtoValidator,
        IQuestionsRepository questionsRepository,
        ILogger<QuestionsService> logger)
    {
        _validator = createQuestionDtoValidator;
        _repository = questionsRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Failure>> Handle(CreateQuestionCommand command, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(command.CreateQuestionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }

        // Валидация бизнес логики
        int openUserQuestionsCount = await _repository.GetOpenUserQuestionsAsync(command.CreateQuestionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            return Errors.Questions.ToManyQuestions().ToFailure();
        }

        // Создание сущности Question
        var questionId = Guid.NewGuid();

        var question = new Question()
        {
            Id = questionId,
            Title = command.CreateQuestionDto.Title,
            Text = command.CreateQuestionDto.Text,
            Tags = command.CreateQuestionDto.TagIds.ToList(),
            UserId = command.CreateQuestionDto.UserId
        };

        // Сохранение сущности Question в БД
        await _repository.AddAsync(question, cancellationToken);

        // Логирование успеха или неуспеха сохранения
        _logger.LogInformation("Question created with id {questionId} successfully", questionId);

        return questionId;
    }
}
