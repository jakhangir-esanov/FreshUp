using FreshUp.Domain.Commons;
using System.Linq.Expressions;

namespace FreshUp.Application.Interfaces;

public interface IRepository<T> where T : Auditable
{
    Task InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> SelectAsync(Expression<Func<T, bool>> expression = null!, string[] includes = null!);
    IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!, bool isNoTracking = true, string[] includes = null!);
    Task SaveAsync();
}
