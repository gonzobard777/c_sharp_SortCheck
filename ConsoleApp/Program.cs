using ConsoleApp.Database;
using ConsoleApp.Domain;

namespace ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        using (var dbContext = new AppDbContext())
        {
            // dbContext.Database.EnsureDeleted();
            // dbContext.Database.EnsureCreated();
            
            SortCols(dbContext);
        }
    }

    public static void SortCols(AppDbContext dbContext)
    {
        var list = dbContext.Set<Cols>()
            .AddSorting("Col1", SortDirection.Descending)
            .AddSorting("Col2", SortDirection.Ascending)
            .AddSorting("Col3", SortDirection.Descending)
            .ToList();

        foreach (var item in list)
        {
            Console.WriteLine($"{item.Col1}\t{item.Col2}\t{item.Col3}");
        }
    }
}