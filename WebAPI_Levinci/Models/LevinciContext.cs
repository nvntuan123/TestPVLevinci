using Microsoft.EntityFrameworkCore;

namespace WebAPI_Levinci.Models
{
    public class LevinciContext : DbContext
    {
        public LevinciContext(DbContextOptions<LevinciContext> options) : base(options)
        { 

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Users> users { get; set; }
    }
}
