using System.Linq.Expressions;

namespace Todo.Service.Application.Services.Interfaces;

public interface IBaseValidationService
{
    Task<bool> ExistsById<TEntity>(int id)
        where TEntity : SimpleDbEntity;
    Task<bool> ExistsById<TEntity>(Guid id)
        where TEntity : ComplexDbEntity;

    Task<bool> ExistsBy<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : DbEntity;
}
