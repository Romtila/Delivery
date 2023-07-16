using System.Linq.Expressions;
using Delivery.BaseLib.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.BaseLib.Infrastructure;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext Context;

    public BaseRepository(DbContext context)
    {
        Context = context;
    }

    public void Add(T entity)
        => Context.Set<T>().Add(entity);

    public void Add(IEnumerable<T> entities)
        => Context.Set<T>().AddRange(entities);

    public void Update(T entity)
        => Context.Set<T>().Update(entity);

    public void Remove(T entity)
        => Context.Set<T>().Remove(entity);

    public T? Find(long id)
        => Context.Set<T>().Find(id);

    public T? Find(Expression<Func<T, bool>> expression)
        => Context.Set<T>().Find(expression);

    public IQueryable<T> Query()
        => Context.Set<T>().AsQueryable();

    public void Commit()
        => Context.SaveChanges();


    public async Task AddAsync(T entity, CancellationToken ct)
        => await Context.Set<T>().AddAsync(entity, ct);

    public async Task AddAsync(IEnumerable<T> entities, CancellationToken ct)
        => await Context.Set<T>().AddRangeAsync(entities, ct);

    public async Task UpdateAsync(T entity, CancellationToken ct)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(T entity, CancellationToken ct)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync(ct);
    }

    public async Task<T?> FindAsync(long id, CancellationToken ct)
        => await Context.Set<T>().FindAsync(id, ct);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> expression, CancellationToken ct)
        => await Context.Set<T>().FindAsync(expression, ct);

    public Task<IQueryable<T>> QueryAsync(CancellationToken ct)
        => Task.FromResult(Context.Set<T>().AsQueryable());
}