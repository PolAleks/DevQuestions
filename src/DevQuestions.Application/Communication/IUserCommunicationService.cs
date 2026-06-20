using CSharpFunctionalExtensions;
using Shared;

namespace DevQuestions.Application.Communication;

public interface IUserCommunicationService
{
    Task<Result<int, Failure>> GetUserRatingAsync(Guid userId, CancellationToken cancellationToken);   
}