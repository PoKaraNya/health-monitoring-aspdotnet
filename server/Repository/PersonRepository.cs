using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using System.Linq.Expressions;

namespace server.Repository;

public class PersonRepository : Repository<Person>, IPersonRepository
{

    public ApplicationDbContext _db;
    public PersonRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public Expression<Func<Person, bool>> GetExpression(int? id)
    {
        if ( id.HasValue)
        {
            return rr => rr.PersonId == id;
        }
        
        return rr => true;
    }

    public async Task<int> GetCountAsync(int? id = null)
    {

        var where = GetExpression(id);
        return await _db.Persons
               .Where(where)
               .CountAsync();
    }

    public void Update(Person obj)
    {
        _db.Persons.Update(obj);
    }
    //public async Task<Person?> GetByIdAsync(int id)
    //{
    //    return await _db.Persons.FirstOrDefaultAsync(x => x.PersonId == id);
    //}
}
