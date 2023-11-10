using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<ProductDto> GetProduct(int id);

        Task<IEnumerable<ProductCategoryDto>> GetProductCategories();

        Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId);
    }
}
