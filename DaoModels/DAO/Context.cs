using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DaoModels.DAO
{
    public class Context : DbContext
    {
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Users> User { get; set; }

        public Context()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WebDispatch;Trusted_Connection=True;");
            }
        }
    }
}
