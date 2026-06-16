using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Question;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(IQuestionsRepository questionsRepository,
                            IValidator<CreateQuestionDto> validator,
                            ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Валидация бизнес логики
        int openUserQuestionsCount = await _questionsRepository.GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);
        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Превышено максимальное количество открытых вопросов");
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
}
