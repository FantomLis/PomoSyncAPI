using Microsoft.EntityFrameworkCore;
using PomoSyncAPI.Backend.Models;

namespace PomoSyncAPI.Backend.Database;

public class MainDatabaseContext : DbContext
{
    public DbSet<User> UserTable { get; set; }
    
    private string _connectionString;

    public MainDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}