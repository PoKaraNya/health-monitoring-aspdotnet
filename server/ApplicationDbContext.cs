using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Authentication;
using server.Models;

namespace server;

public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
    { 
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomRecord> RoomRecords { get; set; }
    public DbSet<PersonRecord> PersonRecords { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        modelBuilder.UseSerialColumns();
    }
}
