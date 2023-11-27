using server.Models;

namespace server.Repository.IRepository;

public interface IPersonRecordRepository : IRepository<PersonRecord>
{
    void Update(PersonRecord obj);
}
