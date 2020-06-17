using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace folio1_app_test.Helpers.DB
{
    public class DataContext : DbContext
    {
        public DbSet<FolioClassDB> FolioClasses { get; set; }
        public DbSet<StudentDB> Students { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
