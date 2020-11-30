using DecoratorPatternExample.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorPatternExample.DataAccessLayer
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}