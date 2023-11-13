using ShopOnline.Server.Repositories.Contracts;
using ShopOnline.Models.Dtos;
using ShopOnline.Services.Contracts;

namespace ShopOnline.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            try
            {
                var product = await this.productsRepository.GetProduct(id);

                if (product is not null)
                {
                    return product;
                }
                else
                {
                    throw new Exception($"Product with id: {id} could not be found.");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            try
            {
                var products = await this.productsRepository.GetProducts();

                if (products is not null)
                {
                    return products;
                }
                else
                {
                    throw new Exception($"No products could be found in the database.");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId)
        {
            try
            {
                var products = await this.productsRepository.GetProductsByCategory(categoryId);

                if (products is not null)
                {
                    return products;
                }
                else
                {
                    throw new Exception($"No products could be found in the database.");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var categories = await this.productsRepository.GetCategories();

                if (categories is not null)
                {
                    return categories;
                }
                else
                {
                    throw new Exception($"No categories could be found in the database.");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }
    }
}
