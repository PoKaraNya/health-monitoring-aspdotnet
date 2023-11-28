namespace server.Repository.IRepository;
public interface IUnitOfWork
{
    IPersonRepository Person { get; }
    IPersonRecordRepository PersonRecord { get; }
    IRoomRepository Room { get; }
    IRoomRecordRepository RoomRecord { get; }
    void Save();
    Task SaveAsync();
}