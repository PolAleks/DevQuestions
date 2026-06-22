using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Question;
using Shared;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : ICommandHandler<QuestionsResponse, GetQuestionsWithFiltersCommand>
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsRepository _tagsRepository;

    public GetQuestionsWithFilters(IQuestionsRepository questionsRepository,
                                   IFilesProvider filesProvider,
                                   ITagsRepository tagsRepository)
    {
        _questionsRepository = questionsRepository;
        _filesProvider = filesProvider;
        _tagsRepository = tagsRepository;
    }

    public async Task<Result<QuestionsResponse, Failure>> Handle(GetQuestionsWithFiltersCommand command, CancellationToken cancellationToken)
    {
        var (questions, count) = await _questionsRepository.GetQuestionsWithFiltersAsync(command, cancellationToken);

        var screenshotsIds = questions.Where(q => q.ScreenshotId is not null)
                                      .Select(q => q.ScreenshotId!.Value);

        var filesDict = await _filesProvider.GetUrlsByIdsAsync(screenshotsIds, cancellationToken);

        var questionTags = questions.SelectMany(q => q.Tags);

        var tags = await _tagsRepository.GetTagsAsync(questionTags, cancellationToken);

        var questionsDto = questions.Select(q => new QuestionDto(
            q.Id,
            q.Title,
            q.Text,
            q.UserId,
            q.ScreenshotId is not null ? filesDict[q.ScreenshotId!.Value] : null,
            q.Solution?.Id,
            tags,
            q.Status.ToRussian()
            ));

            return new QuestionsResponse(questionsDto, count);
    }
}