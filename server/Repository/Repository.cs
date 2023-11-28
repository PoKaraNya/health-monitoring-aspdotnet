using Microsoft.EntityFrameworkCore;
using server.Repository.IRepository;
using System.Linq.Expressions;

namespace server.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }

    public async Task<T> Add(T entity)
    {
        await dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
       // await dbSet.ToListAsync();
        IQueryable<T> query = dbSet;
        return await query.ToListAsync();
    }

    //public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter)
    //{
    //    IQueryable<T> query = dbSet;
    //    query = query.Where(filter);
    //    return await query.FirstOrDefaultAsync();
    //}



    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }
}
