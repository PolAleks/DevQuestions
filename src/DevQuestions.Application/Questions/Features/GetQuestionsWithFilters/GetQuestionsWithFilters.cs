using CSharpFunctionalExtensions;
using DevQuestions.Application.Abstractions;
using DevQuestions.Application.FilesStorage;
using DevQuestions.Application.Tags;
using DevQuestions.Contracts.Questions.Dtos;
using DevQuestions.Contracts.Questions.Responses;
using DevQuestions.Domain.Question;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public class GetQuestionsWithFilters : IQueryHandler<QuestionsResponse, GetQuestionsWithFiltersQuery>
{
    private readonly IFilesProvider _filesProvider;
    private readonly ITagsRepository _tagsRepository;
    private readonly IQuestionReadDbContext _questionDbContext;

    public GetQuestionsWithFilters(IFilesProvider filesProvider,
                                   ITagsRepository tagsRepository,
                                   IQuestionReadDbContext questionDbContext)
    {
        _filesProvider = filesProvider;
        _tagsRepository = tagsRepository;
        _questionDbContext = questionDbContext;
    }

    public async Task<QuestionsResponse> Handle(GetQuestionsWithFiltersQuery query, CancellationToken cancellationToken)
    {
        var questions = await _questionDbContext.ReadQuestions
            .Include(q => q.Solution)
            .Skip(query.GetQuestionDto.Page * query.GetQuestionDto.PageSize)
            .Take(query.GetQuestionDto.PageSize)
            .ToListAsync(cancellationToken);

        var count = await _questionDbContext.ReadQuestions.LongCountAsync(cancellationToken);

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