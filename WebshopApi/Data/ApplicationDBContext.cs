using Microsoft.EntityFrameworkCore;
using WebshopApi.Models;

namespace WebshopApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }

}
