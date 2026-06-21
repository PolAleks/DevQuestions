using DevQuestions.Contracts.Questions;
using FluentValidation;

namespace DevQuestions.Application.Questions.CreateQuestion;

public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок не может быть пустым")
            .MaximumLength(500).WithMessage("Заголовок не должен превышать 500 символов");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст сообщения не может быть пустым.")
            .MaximumLength(5000).WithMessage("Тело вопроса не должно превышать 5000 символов");

        RuleFor(x => x.UserId).NotEmpty();
    }
}