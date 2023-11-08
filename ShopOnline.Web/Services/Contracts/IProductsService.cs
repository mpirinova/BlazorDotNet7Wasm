using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
