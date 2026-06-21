using Shared;

namespace DevQuestions.Application.Questions.Fails;

public partial class Errors
{
    public static class General
    {
        public static Error NotFound(Guid id) => Error.Failure(
            code: "record.not.found",
            message: $"Запись по id - {id} не найдена."
        );
    }

    public static class Questions
    {
        public static Error ToManyQuestions() => Error.Failure(
                code: "to.many.questions",
                message: "Пользователь не может открыть более 3 вопросов."
            );

        public static Error LowRating() => Error.Failure(
            code: "not.enough.rating",
            message: "Недостаточно рейтинга для открытия вопроса."
            );
    }
}