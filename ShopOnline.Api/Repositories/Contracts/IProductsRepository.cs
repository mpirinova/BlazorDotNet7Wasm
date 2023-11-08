using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Repositories.Contracts
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<IEnumerable<ProductCategory>> GetCategories();

        Task<Product> GetProduct(int id);

        Task<ProductCategory> GetCategory(int id);
    }
}
