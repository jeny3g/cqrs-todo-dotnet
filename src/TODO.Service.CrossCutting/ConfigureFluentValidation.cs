using FluentValidation;
using Todo.Service.Application.TodoItems.Commands.Create;

namespace Todo.Service.CrossCutting;

public static class ConfigureFluentValidation
{
    public static IServiceCollection InjectFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTodoItemCommandValidator>();

        return services;
    }
}
