using DevQuestions.Application.Questions;
using DevQuestions.Domain.Question;
using Microsoft.EntityFrameworkCore;

namespace DevQuestions.Infrastructure.Postgres;

public class QuestionsDbContext : DbContext, IQuestionReadDbContext
{
    public DbSet<Question> Questions { get; set; }

    public IQueryable<Question> ReadQuestions => Questions.AsNoTracking().AsQueryable();
}