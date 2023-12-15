using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using server.Utils;
using System.Linq;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace server.Repository;

public class RoomRecordRepository : Repository<RoomRecord>, IRoomRecordRepository
{
    public ApplicationDbContext _db;
    public RoomRecordRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public Expression<Func<RoomRecord, bool>> GetExpression(int? id, bool isOutputOnlyCritical)
    {
        if (isOutputOnlyCritical && id.HasValue)
        {
            return rr => rr.IsCriticalResults == true && rr.RoomId == id;
        }

        if (isOutputOnlyCritical)
        {
            return rr => rr.IsCriticalResults == true;
        }

        if (id.HasValue)
        {
            return rr => rr.RoomId == id;
        }

        return rr => true; // take all fields
    }

    public async Task<IEnumerable<RoomRecord>> GetAllAsync(int pageNumber, bool isOutputOnlyCritical, int? id)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetExpression(id, isOutputOnlyCritical);

        return await _db.RoomRecords
               .Where(where)
               .OrderByDescending(x => x)
               .Skip(skip)
               .Take(take)
               .ToListAsync();
    }

    public async Task<IEnumerable<RoomRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical, int? id)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetExpression(id, isOutputOnlyCritical);

        return await _db.RoomRecords
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
        return await _db.RoomRecords
               .Where(where)
               .CountAsync();
    }

    public async Task<IEnumerable<RoomRecord>> GetAllRoomRecordDashboard(int? day, int? month, int year, int? id)
    {
        var roomRecords = _db.RoomRecords
            .Include(nameof(Room))
            .AsQueryable();

        Expression<Func<RoomRecord, bool>> dateCondition = record =>
           (!day.HasValue || record.RecordedDate.Day == day.Value) &&
           (!month.HasValue || record.RecordedDate.Month == month.Value) &&
           record.RecordedDate.Year == year;

        roomRecords = roomRecords.Where(dateCondition);

        if (id.HasValue)
        {
            roomRecords = roomRecords.Where(record => record.RoomId == id.Value);
        }

        return await roomRecords.ToListAsync();
    }


    void IRoomRecordRepository.Update(RoomRecord obj)
    {
        throw new NotImplementedException();
    }

 
}
