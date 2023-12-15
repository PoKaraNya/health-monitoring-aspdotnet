using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using server.Utils;
using System.Linq.Expressions;

namespace server.Repository;

public class PersonRecordRepository(ApplicationDbContext db) : Repository<PersonRecord>(db), IPersonRecordRepository
{
    public ApplicationDbContext _db = db;

    public Expression<Func<PersonRecord, bool>> GetExpression(int? id, bool isOutputOnlyCritical)
    {
        if (isOutputOnlyCritical && id.HasValue)
        {
            return rr => rr.IsCriticalResults == true && rr.PersonId == id;
        }

        if (isOutputOnlyCritical)
        {
            return rr => rr.IsCriticalResults == true;
        }

        if (id.HasValue)
        {
            return rr => rr.PersonId == id;
        }
        
        return rr => true; // take all fields
    }

    public async Task<IEnumerable<PersonRecord>> GetAllAsync(int pageNumber, bool isOutputOnlyCritical, int? id = null)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetExpression(id, isOutputOnlyCritical);
        return await _db.PersonRecords
               .Where(where)
               .OrderByDescending(x => x)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
    }

    public async Task<IEnumerable<PersonRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical, int? id = null)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetExpression(id, isOutputOnlyCritical);
        return await _db.PersonRecords
               .Include(nameof(Person))
               .Include(nameof(Room))
               .Where(where)
               .OrderByDescending(x => x)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
    }

    public async Task<int> GetCountAsync(bool isOutputOnlyCritical, int? id = null)
    {
        var where = GetExpression(id, isOutputOnlyCritical);
        return await _db.PersonRecords
               .Where(where)
               .CountAsync();
    }

    public async Task<IEnumerable<PersonRecord>> GetAllPersonRecordDashboard(int? day, int? month, int year, int? id)
    {
        var personRecords = _db.PersonRecords.Include(nameof(Room)).Include(nameof(Person)).AsQueryable();

        Expression<Func<PersonRecord, bool>> dateCondition = record =>
           (!day.HasValue || record.RecordedDate.Day == day.Value) &&
           (!month.HasValue || record.RecordedDate.Month == month.Value) &&
           record.RecordedDate.Year == year;

        personRecords = personRecords.Where(dateCondition);

        if (id.HasValue)
        {
            personRecords = personRecords.Where(record => record.PersonId == id.Value);
        }

        return await personRecords.ToListAsync();
    }


public void Update(PersonRecord obj)
    {
       //await _db.PersonRecords.UpdateAsync(obj);
       // await _db.SaveChangesAsync(obj);
        //return obj;
    }

}