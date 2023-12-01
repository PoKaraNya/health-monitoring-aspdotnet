using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using server.Utils;
using System.Linq.Expressions;

namespace server.Repository;

public class PersonRecordRepository(ApplicationDbContext db) : Repository<PersonRecord>(db), IPersonRecordRepository
{
    public ApplicationDbContext _db = db;

    public Expression<Func<PersonRecord, bool>> GetIsCriticalExpression(bool isOutputOnlyCritical)
    {
        if (isOutputOnlyCritical)
        {
            return rr => rr.IsCriticalResults == true;
        }
        else
        {
            return rr => true; // take all fields
        }
    }

    public async Task<IEnumerable<PersonRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetIsCriticalExpression(isOutputOnlyCritical);
        //return await _db.PersonRecords.ToListAsync(); ;
        return await _db.PersonRecords
               .Include(nameof(Person))
               .Include(nameof(Room))
               .Where(where)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
    }

    public void Update(PersonRecord obj)
    {
       //await _db.PersonRecords.UpdateAsync(obj);
       // await _db.SaveChangesAsync(obj);
        //return obj;
    }
}
