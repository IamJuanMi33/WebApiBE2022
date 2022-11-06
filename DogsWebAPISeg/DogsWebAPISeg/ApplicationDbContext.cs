using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DogsWebAPISeg.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsWebAPISeg
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DogKennel>()
                .HasKey(d => new { d.DogId, d.KennelId });
        }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Kennel> Kennels { get; set; }

        public DbSet<DogKennel> DogKennel { get; set; }

    }
}
