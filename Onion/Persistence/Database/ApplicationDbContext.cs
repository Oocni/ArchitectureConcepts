using Domain.Observations;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Observation> Observations { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Observation>().HasKey(o => o.Id);
        modelBuilder.Entity<Observation>().Property(o => o.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Observation>().Property(o => o.Name).IsRequired();
        modelBuilder.Entity<Observation>().Property(o => o.Description).IsRequired();
        modelBuilder.Entity<Observation>().Property(o => o.CreatedDate).IsRequired();
        modelBuilder.Entity<Observation>().Property(o => o.CreatedBy).IsRequired();
        modelBuilder.Entity<Observation>().Property(o => o.DeletedDate).IsRequired(false);
        modelBuilder.Entity<Observation>().Property(o => o.DeletedBy).IsRequired(false);
    }
}