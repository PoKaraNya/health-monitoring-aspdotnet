﻿using Microsoft.EntityFrameworkCore;
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
        //IQueryable<T> query = dbSet;
        //return await query.ToListAsync();
        return await dbSet.ToListAsync();
    }


    public async Task<T?> GetFirstOrDefault(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<T?> UpdateAsync(T entity, Expression<Func<T, bool>> filter)
    {
        //IQueryable<T> query = dbSet;
        //query = query.Where(filter);
        var existing = await dbSet.SingleOrDefaultAsync(filter);
        if (existing != null)
        {
            dbSet.Entry(existing).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
            return existing;
        }
        return null;
    }
    //public void RemoveRange(IEnumerable<T> entities)
    //{
    //    dbSet.RemoveRange(entities);
    //}

    public async Task<T?> DeleteAsync(Expression<Func<T, bool>> filter)
    {
        var existing = await dbSet.FirstOrDefaultAsync(filter);
        if (existing != null)
        {
            dbSet.Remove(existing);
            await _db.SaveChangesAsync();
            return existing;
        }
        return null;
    }
}
