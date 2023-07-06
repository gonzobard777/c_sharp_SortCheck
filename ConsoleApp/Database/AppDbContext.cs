using Microsoft.EntityFrameworkCore;
using ConsoleApp.Domain;

namespace ConsoleApp.Database;

public class AppDbContext : DbContext
{
    public DbSet<Cols> Cols { get; set; }
    public DbSet<Population> Population { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("host=127.0.0.1;port=6748;database=db;username=root;password=12345");
        // optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TestGisOnlineDb;Trusted_Connection=True;Encrypt=False;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cols>().HasData(
            new() { Id = 1, Col1 = 2, Col2 = 1, Col3 = 2 },
            new() { Id = 2, Col1 = 3, Col2 = 2, Col3 = 3 },
            new() { Id = 3, Col1 = 2, Col2 = 2, Col3 = 1 },
            new() { Id = 4, Col1 = 1, Col2 = 2, Col3 = 1 },
            new() { Id = 5, Col1 = 1, Col2 = 2, Col3 = 3 },
            new() { Id = 6, Col1 = 3, Col2 = 1, Col3 = 3 },
            new() { Id = 7, Col1 = 1, Col2 = 1, Col3 = 2 },
            new() { Id = 8, Col1 = 2, Col2 = 2, Col3 = 3 },
            new() { Id = 9, Col1 = 1, Col2 = 1, Col3 = 1 },
            new() { Id = 10, Col1 = 3, Col2 = 2, Col3 = 1 },
            new() { Id = 11, Col1 = 2, Col2 = 1, Col3 = 3 },
            new() { Id = 12, Col1 = 1, Col2 = 2, Col3 = 2 },
            new() { Id = 13, Col1 = 3, Col2 = 1, Col3 = 1 },
            new() { Id = 14, Col1 = 1, Col2 = 1, Col3 = 3 },
            new() { Id = 15, Col1 = 3, Col2 = 1, Col3 = 2 },
            new() { Id = 16, Col1 = 3, Col2 = 2, Col3 = 2 },
            new() { Id = 17, Col1 = 2, Col2 = 2, Col3 = 2 },
            new() { Id = 18, Col1 = 2, Col2 = 1, Col3 = 1 }
        );

        modelBuilder.Entity<Population>().HasData(
            new() { Id = 1, Year = 2017, Month = 11, Counter = 33018846 },
            new() { Id = 2, Year = 2018, Month = 2, Counter = 31349520 },
            new() { Id = 3, Year = 2017, Month = 9, Counter = 35339253 },
            new() { Id = 4, Year = 2018, Month = 5, Counter = 34981100 },
            new() { Id = 5, Year = 2018, Month = 1, Counter = 34854035 },
            new() { Id = 6, Year = 2018, Month = 3, Counter = 36131130 },
            new() { Id = 7, Year = 2017, Month = 10, Counter = 35400265 },
            new() { Id = 8, Year = 2018, Month = 4, Counter = 32640401 },
            new() { Id = 9, Year = 2017, Month = 12, Counter = 37464014 }
        );
    }
}