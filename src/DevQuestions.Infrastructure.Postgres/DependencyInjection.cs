using DevQuestions.Application.Questions;
using DevQuestions.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<QuestionsDbContext>();
        services.AddScoped<IQuestionsRepository, QuestionsEfCoreRepository>();
        return services;
    }
    
}