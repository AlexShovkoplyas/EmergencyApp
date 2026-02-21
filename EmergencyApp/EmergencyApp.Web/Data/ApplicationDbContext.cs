using EmergencyApp.Web.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentitySchemaDbContext<ApplicationDbContext>(options)
{
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    public DbSet<UserAddress> UserAddresses => Set<UserAddress>();
    public DbSet<ContactPerson> ContactPersons => Set<ContactPerson>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserSettings>(entity =>
        {
            entity.ToTable("UserSettings", "settings");

            entity.HasOne(us => us.User)
                .WithMany()
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(us => us.Address)
                .WithMany()
                .HasForeignKey(us => us.AddressId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(us => us.ContactPerson)
                .WithMany()
                .HasForeignKey(us => us.ContactPersonId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<UserAddress>().ToTable("UserAddresses", "settings");
        builder.Entity<ContactPerson>().ToTable("ContactPersons", "settings");
    }
}
