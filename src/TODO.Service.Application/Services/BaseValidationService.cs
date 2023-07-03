using System.Linq.Expressions;

public class BaseValidationService : IBaseValidationService
{
    private readonly ITodoContext _context;

    public BaseValidationService(ITodoContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsById<TEntity>(int id) where TEntity : SimpleDbEntity
    {
        var dbSet = _context.Set<TEntity>();

        var exits = await dbSet.AnyAsync(e => e.Id == id);

        return exits;
    }

    public async Task<bool> ExistsById<TEntity>(Guid id) where TEntity : ComplexDbEntity
    {
        var dbSet = _context.Set<TEntity>();

        var exits = await dbSet.AnyAsync(e => e.Id == id);

        return exits;
    }

    public async Task<bool> ExistsBy<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : DbEntity
    {
        if (filter is null)
            return false;

        var dbSet = _context.Set<TEntity>();

        var exits = await dbSet
            .Where(filter)
            .AnyAsync();

        return exits;
    }
}
