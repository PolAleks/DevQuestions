using DevQuestions.Application.Exceptions;

namespace DevQuestions.Application.Questions.Fails.Exceptions
{
    public class ToManyQuestionException() 
        : BadRequestException([Errors.Question.ToManyQuestions()])
    {
    }
}