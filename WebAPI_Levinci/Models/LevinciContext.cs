using Microsoft.EntityFrameworkCore;

namespace WebAPI_Levinci.Models
{
    public class LevinciContext : DbContext
    {
        public LevinciContext(DbContextOptions<LevinciContext> options) : base(options)
        { 

        }

        public const string strConnectString = @"Data Source=TUANIT\SQLEXPRESS;Initial Catalog=LevinciDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(strConnectString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Users> users { get; set; }
    }
}
