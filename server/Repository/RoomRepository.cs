using server.Models;
using server.Repository.IRepository;

namespace server.Repository;

public class RoomRepository : Repository<Room>, IRoomRepository
{
    public ApplicationDbContext _db;
    public RoomRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Room obj)
    {
        _db.Rooms.Update(obj);
    }
}
