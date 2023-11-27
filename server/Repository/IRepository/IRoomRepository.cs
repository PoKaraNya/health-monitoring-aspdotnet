using server.Models;

namespace server.Repository.IRepository;

public interface IRoomRepository: IRepository<Room>
{
    void Update(Room obj);
}
