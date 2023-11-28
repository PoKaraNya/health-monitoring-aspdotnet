﻿using System.Linq.Expressions;
using System.Threading.Tasks;

namespace server.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<T> Add(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter);
    Task<T?> UpdateAsync(T entity, Expression<Func<T, bool>> filter);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}