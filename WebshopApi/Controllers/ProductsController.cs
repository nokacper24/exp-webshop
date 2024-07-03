using Microsoft.AspNetCore.Mvc;
using WebshopApi.Repos;

namespace WebshopApi.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository productsRepo;

        public ProductsController(ProductsRepository productsRepo)
        {
            this.productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = await productsRepo.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var product = await productsRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
