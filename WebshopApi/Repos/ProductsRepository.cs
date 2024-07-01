using Microsoft.EntityFrameworkCore;
using WebshopApi.Data;
using WebshopApi.Models;

namespace WebshopApi.Repos
{
    public class ProductsRepository
    {
        private readonly ApplicationDBContext dbContext;
        public ProductsRepository(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }
    }
}