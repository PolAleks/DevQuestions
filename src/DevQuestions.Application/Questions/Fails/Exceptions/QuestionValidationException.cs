using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions
{
    public class QuestionValidationException(Error[] errors) : BadRequestException(errors)
    {
    }
}