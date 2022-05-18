using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Context
{
    public class FundooDbContext : DbContext
    {
        public FundooDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> user { get; set; }
        public DbSet<Note> notes{ get; set; }
        public DbSet<Label> Label { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        }

    }
}
