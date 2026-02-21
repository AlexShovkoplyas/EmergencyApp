using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmergencyApp.Web.Data;

public abstract class IdentitySchemaDbContext<TContext>(DbContextOptions<TContext> options)
    : IdentityDbContext(options)
    where TContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUser>().ToTable("Users", "Identity");
        builder.Entity<IdentityRole>().ToTable("Roles", "Identity");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Identity");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Identity");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Identity");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Identity");
    }
}
