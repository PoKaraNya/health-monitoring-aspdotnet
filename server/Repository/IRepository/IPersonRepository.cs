using server.Models;

namespace server.Repository.IRepository;

public interface IPersonRepository: IRepository<Person>
{
    void Update(Person obj);
    Task<int> GetCountAsync(int? id = null);
    //Task<Person?> GetByIdAsync(int id);
}
