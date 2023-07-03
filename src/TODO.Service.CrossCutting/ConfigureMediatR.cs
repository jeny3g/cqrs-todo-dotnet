using System.Diagnostics.CodeAnalysis;
using Todo.Service.Application.TodoItems.Queries.Get;

[assembly: ExcludeFromCodeCoverage]
namespace Todo.Service.CrossCutting;

public static class ConfigureMediatR
{
    public static IServiceCollection InjectMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ListTodoItemQuery>());

        return services;
    }
}
