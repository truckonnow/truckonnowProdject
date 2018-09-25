using Microsoft.EntityFrameworkCore;
using Parser.Dao.Models;
using Parser.DAO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.DAO
{
    class ContextP : DbContext
    {
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Users> User { get; set; }

        public ContextP()
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
