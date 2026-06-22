using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions.Dtos;

namespace DevQuestions.Application.Questions.Features.GetQuestionsWithFilters;

public record GetQuestionsWithFiltersQuery(GetQuestionDto GetQuestionDto) : IQuery;
