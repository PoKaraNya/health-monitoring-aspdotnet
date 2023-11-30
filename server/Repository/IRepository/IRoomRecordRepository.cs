using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Repository.IRepository;
public interface IRoomRecordRepository : IRepository<RoomRecord>
{
    void Update(RoomRecord obj);
    Task<List<RoomRecord>> GetAllWithRelationsAsync();
}
