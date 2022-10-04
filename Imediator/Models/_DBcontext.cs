using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Imediator.Models
{
    public class _DBcontext : DbContext
    {
        public _DBcontext(DbContextOptions<_DBcontext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=Test;Trusted_Connection=True;MultipleActiveResultSets=True");
        }



    }
}
