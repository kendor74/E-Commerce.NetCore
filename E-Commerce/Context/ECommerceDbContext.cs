using E_Commerce.Model;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Context
{
    public class ECommerceDbContext: DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) :base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Brand> Brand{ get; set; }
        public DbSet<Product> Product{ get; set; }
        public DbSet<Color> Color{ get; set; }
        public DbSet<Image> Image{ get; set; }
    }
}
