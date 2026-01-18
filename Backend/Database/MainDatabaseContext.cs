using Microsoft.EntityFrameworkCore;
using PomoSyncAPI.Backend.Models;

namespace PomoSyncAPI.Backend.Database;

public class MainDatabaseContext : DbContext
{
    public const string CONNECTION_STRING_NAME = "MainDb";
    public DbSet<User> UserTable { get; set; }

    public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options) : base(options){}
}