using DaoModels.DAO.Models;
using Microsoft.EntityFrameworkCore;

namespace DaoModels.DAO
{
    public class Context : DbContext
    {
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<VehiclwInformation> VehiclwInformation { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Ask> Asks { get; set; }
        public DbSet<PhotoInspection> PhotoInspections { get; set; }
        public DbSet<Ask1> Ask1s { get; set; }
        public DbSet<AskFromUser> AskFromUsers { get; set; }

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