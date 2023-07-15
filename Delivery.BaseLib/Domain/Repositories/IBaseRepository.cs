using System.Linq.Expressions;

namespace Delivery.BaseLib.Domain.Repositories;

public interface IBaseRepository<T> where T : class
{
    void Add(T entity);
    void Add(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    T? Find(long id);
    T? Find(Expression<Func<T, bool>> expression);
    IQueryable<T> Query();

    Task AddAsync(T entity, CancellationToken ct);
    Task AddAsync(IEnumerable<T> entities, CancellationToken ct);
    Task UpdateAsync(T entity, CancellationToken ct);
    Task RemoveAsync(T entity, CancellationToken ct);
    Task<T?> FindAsync(long id, CancellationToken ct);
    Task<T?> FindAsync(Expression<Func<T, bool>> expression, CancellationToken ct);
    Task<IQueryable<T>> QueryAsync(CancellationToken ct);
}