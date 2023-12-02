using Microsoft.EntityFrameworkCore;
using server.Models;
using System.Linq.Expressions;

namespace server.Repository.IRepository;
public interface IRoomRecordRepository : IRepository<RoomRecord>
{
    void Update(RoomRecord obj);

    Expression<Func<RoomRecord, bool>> GetExpression(int? id, bool isOutputOnlyCritical);
    Task<IEnumerable<RoomRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical, int? id = null);
    Task<IEnumerable<RoomRecord>> GetAllRoomRecordDashboard(int? day, int? month, int year,int? id);
}
