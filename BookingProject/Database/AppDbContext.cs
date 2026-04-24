using Microsoft.EntityFrameworkCore;

namespace BookingProject.Database;

//sqllite db
public class AppDbContext:DbContext
{
    public DbSet<Property> Property { get; set; }
    public DbSet<User> User { get; set; }

    public string DbPath = "C:\\Users\\tony_\\OneDrive - BTH Student\\lexicon .NET\\project\\code\\BookingSystem\\BookingProject";
    
    public AppDbContext()
    {
        /*var folder=Environment.SpecialFolder.LocalApplicationData;
        var path=Environment.GetFolderPath(folder);
        DbPath=Path.Combine(path,"BookingAppDbContext.db");
        Database.EnsureCreated();*/

        DbPath = Path.Combine(DbPath, "BookingAppDbContext.db");
        // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}