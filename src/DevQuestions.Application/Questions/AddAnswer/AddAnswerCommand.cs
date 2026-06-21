using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;

namespace DevQuestions.Application.Questions.AddAnswer;

public record AddAnswerCommand(Guid QuestionId, AddAnswerDto AnswerDto) : ICommand;
