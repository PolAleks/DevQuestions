using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Application.Questions.CreateQuestion;

public record CreateQuestionCommand(CreateQuestionDto CreateQuestionDto) : ICommand;
