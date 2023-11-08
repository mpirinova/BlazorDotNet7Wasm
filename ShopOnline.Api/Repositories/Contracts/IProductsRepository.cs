using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<IEnumerable<ProductCategory>> GetCategories();

        Task<Product> GetProduct(int id);

        Task<ProductCategory> GetCategory(int id);
    }
}
