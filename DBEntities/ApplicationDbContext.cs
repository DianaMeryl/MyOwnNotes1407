using DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace DBEntites
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder       // One - To - Many
        //       .Entity<ApplicationUser>()
        //       .HasMany(x => x.Notes)
        //       .WithOne(x => x.ApplicationUsers)
        //       .HasForeignKey(x => x.UserId)
        //       .HasPrincipalKey(x => x.Id);
        //}

    }



}