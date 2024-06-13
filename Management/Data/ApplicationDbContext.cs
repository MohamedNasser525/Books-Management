using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Management.Models;
using Humanizer.Localisation;
namespace Management.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //builder.HasDefaultSchema("");
            builder.Entity<AppUser>().ToTable("Users","security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity < IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity < IdentityUserClaim<string>>().ToTable("UserClaims", "security"); 
            builder.Entity < IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");


    }
        public DbSet<book> Books { get; set; }

    }
}
