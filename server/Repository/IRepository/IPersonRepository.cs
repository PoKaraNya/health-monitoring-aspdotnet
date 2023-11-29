using server.Models;

namespace server.Repository.IRepository;

public interface IPersonRepository: IRepository<Person>
{
    void Update(Person obj);
    //Task<Person?> GetByIdAsync(int id);
}
