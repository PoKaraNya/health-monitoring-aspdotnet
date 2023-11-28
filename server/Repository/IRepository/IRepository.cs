using System.Linq.Expressions;

namespace server.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    //T GetFirstOrDefault(Expression<Func<T, bool>> filter);
    Task<T> Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
