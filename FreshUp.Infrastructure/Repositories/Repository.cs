using FreshUp.Infrastructure.Contexts;
using FreshUp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using FreshUp.Domain.Commons;

namespace FreshUp.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    public readonly AppDbContext appDbContext;
    public readonly DbSet<T> dbSet;
    public Repository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        this.dbSet = appDbContext.Set<T>();
    }

    public async Task InsertAsync(T entity)
    {
        await this.dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        this.appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        this.dbSet.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression = null!, string[] includes = null!)
    {
        IQueryable<T> query = dbSet.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return (await query.FirstOrDefaultAsync(expression))!;
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!, bool isNoTracking = true, string[] includes = null!)
    {
        IQueryable<T> query = expression is null ? dbSet : dbSet.Where(expression);

        query = isNoTracking ? query.AsNoTracking() : query;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }

    public async Task SaveAsync()
    {
        await this.appDbContext.SaveChangesAsync();
    }
}
