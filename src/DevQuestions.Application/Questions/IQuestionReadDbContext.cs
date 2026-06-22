using DevQuestions.Domain.Question;

namespace DevQuestions.Application.Questions;

public interface IQuestionReadDbContext
{
    IQueryable<Question> ReadQuestions { get; }
}