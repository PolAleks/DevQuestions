namespace DevQuestions.Application.Tags;

public interface ITagsRepository
{
    Task<IReadOnlyList<string>> GetTagsAsync(IEnumerable<Guid> tagIds, CancellationToken cancellationToken);     
}