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
            var producsts = await productsRepo.GetAllAsync();
            return Ok(producsts);
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
