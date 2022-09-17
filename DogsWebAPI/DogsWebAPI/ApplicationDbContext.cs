using DogsWebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DogsWebAPI
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Kennel> Kennels { get; set; }
    }
}
