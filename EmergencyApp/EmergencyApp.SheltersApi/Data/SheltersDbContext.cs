using EmergencyApp.SheltersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.SheltersApi.Data;

public class SheltersDbContext(DbContextOptions<SheltersDbContext> options) : DbContext(options)
{
    public DbSet<BombShelter> BombShelters => Set<BombShelter>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BombShelter>(e =>
        {
            e.HasKey(s => s.Id);
            e.Property(s => s.Name).HasMaxLength(200).IsRequired();
            e.Property(s => s.Address).HasMaxLength(300);
            e.Property(s => s.Description).HasMaxLength(2000);
        });
    }
}
