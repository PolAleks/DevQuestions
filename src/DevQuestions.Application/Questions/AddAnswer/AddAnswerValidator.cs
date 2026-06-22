using DevQuestions.Contracts.Questions.Dtos;
using FluentValidation;

namespace DevQuestions.Application.Questions.AddAnswer;

public class AddAnswerValidator : AbstractValidator<AddAnswerDto>
{
    public AddAnswerValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст ответа не может быть пустым")
            .MaximumLength(5000).WithMessage("Ответ не должен превышать 5000 символов");
    }
}