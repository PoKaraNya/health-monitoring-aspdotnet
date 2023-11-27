using server.Repository.IRepository;

namespace server.Repository;

public class UnitOfWork : IUnitOfWork
{
    public ApplicationDbContext _db;
    public IPersonRepository Person { get; private set; }
    public IPersonRecordRepository PersonRecord { get; private set; }
    public IRoomRepository Room { get; private set; }
    public IRoomRecordRepository RoomRecord { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Person = new PersonRepository(_db);
        PersonRecord = new PersonRecordRepository(_db);
        Room = new RoomRepository(_db);
        RoomRecord = new RoomRecordRepository(_db);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}
