using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<IEnumerable<ProductCategoryDto>> GetCategories();

        Task<ProductDto> GetProduct(int id);

        Task<ProductCategory> GetCategory(int id);

        Task<IEnumerable<ProductDto>> GetProductsByCategory(int id);
    }
}
