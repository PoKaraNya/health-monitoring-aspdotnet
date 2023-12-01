using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using server.Utils;
using System.Linq;
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
        Expression<Func<RoomRecord, bool>> expression = rr => rr.IsCriticalResults == isOutputOnlyCritical;
        return expression;
    }

    public async Task<IEnumerable<RoomRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical)
    {
        var take = Constants.MaxItemsPerPage;
        var skip = (pageNumber - 1) * take;
        var where = GetIsCriticalExpression(isOutputOnlyCritical);

        return await _db.RoomRecords
               .Include(nameof(Room))
               .Skip(skip)
               .Take(take)
               .Where(where)
               .ToListAsync();
    }
   

    //public void Update(RoomRecord obj)
    //{
    //    _db.RoomRecords.Update(obj);
    //}

    //Task<RoomRecord> IRoomRecordRepository.Update(RoomRecord obj)
    //{
    //    throw new NotImplementedException();
    //}
    void IRoomRecordRepository.Update(RoomRecord obj)
    {
        throw new NotImplementedException();
    }
}
