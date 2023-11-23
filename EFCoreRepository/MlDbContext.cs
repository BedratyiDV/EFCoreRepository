using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRepository
{
    public class MlDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MlDbContext()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = movies.db");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
