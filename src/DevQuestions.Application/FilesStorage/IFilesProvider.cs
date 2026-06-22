namespace DevQuestions.Application.FilesStorage;

public interface IFilesProvider
{
    Task<string> UploadAsync(Stream stream, string key, string bucket);

    Task<string> GetByUrlIdAsync(Guid fileId, CancellationToken cancellationToken);

    Task<Dictionary<Guid, string>> GetUrlsByIdsAsync(IEnumerable<Guid> fileIds, CancellationToken cancellationToken);
}
