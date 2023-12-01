using server.Models;
using System.Linq.Expressions;

namespace server.Repository.IRepository;

public interface IPersonRecordRepository : IRepository<PersonRecord>
{
    void Update(PersonRecord obj);

    Expression<Func<PersonRecord, bool>> GetIsCriticalExpression(bool isOutputOnlyCritical);
    Task<IEnumerable<PersonRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical);
}
