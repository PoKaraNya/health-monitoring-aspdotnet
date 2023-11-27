using server.Models;
using server.Repository.IRepository;

namespace server.Repository;

public class PersonRepository : Repository<Person>, IPersonRepository
{

    public ApplicationDbContext _db;
    public PersonRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Person obj)
    {
        _db.Persons.Update(obj);
    }
}
