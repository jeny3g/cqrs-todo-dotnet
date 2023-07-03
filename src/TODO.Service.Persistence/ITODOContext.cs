using System.Diagnostics.CodeAnalysis;

[assembly: ExcludeFromCodeCoverage]
namespace Todo.Service.Persistence;

public interface ITodoContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<TodoItem> TodoItems { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
