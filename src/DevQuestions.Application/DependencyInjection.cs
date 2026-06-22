using DevQuestions.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuestions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Отключили IQuestionsService так как перешли на работу через обработчики команд
        // services.AddScoped<IQuestionsService, QuestionsService>();

        // Способ 1 - регистрируем конкретные обработчики для команд
        // services.AddScoped<ICommandHandler<Guid, CreateQuestionCommand>, CreateQuestionHandler>();
        // services.AddScoped<ICommandHandler<Guid, AddAnswerCommand>, AddAnswerHandler>();

        // Способ 2 - регистрируем обработчики команд c помощью nuget пакета Scrutor
        var assembly = typeof(DependencyInjection).Assembly;
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}