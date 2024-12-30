using Microsoft.EntityFrameworkCore;
using PR3__Potapov_.Item;

public class AppDbContext : DbContext
{
    public DbSet<WatchlistItem> WatchlistItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WatchlistItem>().HasKey(w => w.Id);
    }
}
