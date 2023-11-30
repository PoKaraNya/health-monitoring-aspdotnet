using Microsoft.EntityFrameworkCore;
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

    public async Task<List<RoomRecord>> GetAllWithRelationsAsync()
    {
        ///var roomRecords = await _unitOfWork.RoomRecord.Include(nameof(Room)).ToListAsync();
        return await _db.RoomRecords
               .Include(nameof(Room)) // Включаем данные из связанной таблицы Room
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
