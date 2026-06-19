using Shared;

namespace DevQuestions.Application.Questions.Fails;

public partial class Errors
{
    public static class Question
    {
        public static Error ToManyQuestions() => Error.Failure(
                code: "to.many.questions",
                message: "Пользователь не может открыть более 3 вопросов."
            );
    }
}