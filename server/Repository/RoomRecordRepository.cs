using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using server.Utils;
using System.Linq.Expressions;

namespace server.Repository;

public class RoomRecordRepository : Repository<RoomRecord>, IRoomRecordRepository
{
    public ApplicationDbContext _db;
    public RoomRecordRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public Expression<Func<RoomRecord, bool>> GetIsCriticalExpression(bool isOutputOnlyCritical)
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

    public async Task<IEnumerable<RoomRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetIsCriticalExpression(isOutputOnlyCritical);

        return await _db.RoomRecords
               .Include(nameof(Room))
               .Where(where)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
    }
   
    void IRoomRecordRepository.Update(RoomRecord obj)
    {
        throw new NotImplementedException();
    }
}
