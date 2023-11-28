using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Repository.IRepository;
using System.Linq.Expressions;

namespace server.Repository;

public class PersonRecordRepository : Repository<PersonRecord>, IPersonRecordRepository
{
    public ApplicationDbContext _db;
    public PersonRecordRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(PersonRecord obj)
    {
       //await _db.PersonRecords.UpdateAsync(obj);
       // await _db.SaveChangesAsync(obj);
        //return obj;
    }

   
}
