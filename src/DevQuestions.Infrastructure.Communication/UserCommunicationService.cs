using CSharpFunctionalExtensions;
using DevQuestions.Application.Communication;
using Shared;

namespace DevQuestions.Infrastructure.Communication;

public class UserCommunicationService : IUserCommunicationService
{
    public Task<Result<int, Failure>> GetUserRatingAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
