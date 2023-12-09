using server.Models;
using System.Linq.Expressions;

namespace server.Repository.IRepository;

public interface IPersonRecordRepository : IRepository<PersonRecord>
{
    void Update(PersonRecord obj);

    Expression<Func<PersonRecord, bool>> GetExpression(int? id, bool isOutputOnlyCritical);
    Task<IEnumerable<PersonRecord>> GetAllAsync(int pageNumber, bool isOutputOnlyCritical, int? id = null);
    Task<IEnumerable<PersonRecord>> GetAllWithRelationsAsync(int pageNumber, bool isOutputOnlyCritical, int? id = null);
    Task<IEnumerable<PersonRecord>> GetAllPersonRecordDashboard(int? day, int? month, int year, int? id);
    Task<int> GetCountAsync(bool isOutputOnlyCritical, int? id = null);
}
