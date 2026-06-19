using FluentValidation.Results;
using Shared;

namespace DevQuestions.Application.Extensions;

public static class ValidationExtentions
{
    public static Failure ToErrors(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(e => Error.Validation(
                e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToList();
    }
}