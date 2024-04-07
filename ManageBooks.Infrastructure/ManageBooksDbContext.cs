using ManageBooks.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ManageBooks.Infrastructure;

public class ManageBooksDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<Assessment> Assessment { get; set; }

    public ManageBooksDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()
        );
    }
}
