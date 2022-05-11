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

        public DbSet<Note> note{ get; set; }
    }
}
