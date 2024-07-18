using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebshopApi.Models;

namespace WebshopApi.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Sample prouct 1",
                Price = 100
            },
            new Product
            {
                Id = 2,
                Name = "Sample prouct 2",
                Price = 50
            }
            );
            base.OnModelCreating(builder);
        }
    }

}
