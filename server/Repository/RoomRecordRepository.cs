using server.Models;
using server.Repository.IRepository;

namespace server.Repository;

public class RoomRecordRepository : Repository<RoomRecord>, IRoomRecordRepository
{
    public ApplicationDbContext _db;
    public RoomRecordRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
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
