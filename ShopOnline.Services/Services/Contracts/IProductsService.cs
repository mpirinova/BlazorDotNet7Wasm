using ShopOnline.Models.Dtos;

namespace ShopOnline.Services.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetProduct(int id);

        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();

        Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    }
}
