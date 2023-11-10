using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Repositories;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await this.productsRepository.GetProducts();

                if (products is null)
                {
                    return NotFound();
                }

                return Ok(products);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await this.productsRepository.GetProduct(id);

                if (product is null)
                {
                    return BadRequest();
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await this.productsRepository.GetCategories();

                return Ok(productCategories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("{categoryId}/productsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await this.productsRepository.GetProductsByCategory(categoryId);
                
                return Ok(products);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }
    }
}
