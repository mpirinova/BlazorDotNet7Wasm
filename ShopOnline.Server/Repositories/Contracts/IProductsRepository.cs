using ShopOnline.Server.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Server.Repositories.Contracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<IEnumerable<ProductCategoryDto>> GetCategories();

        Task<ProductDto> GetProduct(int id);

        Task<ProductCategoryDto> GetCategory(int id);

        Task<IEnumerable<ProductDto>> GetProductsByCategory(int id);
    }
}
