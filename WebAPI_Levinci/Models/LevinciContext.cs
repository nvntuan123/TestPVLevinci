using Microsoft.EntityFrameworkCore;

namespace WebAPI_Levinci.Models
{
    public class LevinciContext : DbContext
    {
        public DbSet<Users> users { get; set; }

        public const string strConnectString = @"Data Source=TUANIT\SQLEXPRESS;Initial Catalog=LevinciDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(strConnectString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
