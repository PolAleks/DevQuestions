using DevQuestions.Application.Exceptions;
using Shared;

namespace DevQuestions.Application.Questions.Fails.Exceptions;

public class QuestionNotFoundException(Error[] errors) : NotFoundException(errors)
{
}