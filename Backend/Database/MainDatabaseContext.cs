using Microsoft.EntityFrameworkCore;
using PomoSyncAPI.Backend.Models;

namespace PomoSyncAPI.Backend.Database;

public class MainDatabaseContext : DbContext
{
    public const string CONNECTION_STRING_NAME = "MainDb";
    public DbSet<User> UserTable { get; set; }
    
    private string _connectionString;

    public MainDatabaseContext() {}
    public MainDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MainDatabaseContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(CONNECTION_STRING_NAME)!;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}