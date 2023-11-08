using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ProductsRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            return await this.dbContext.ProductCategories.ToListAsync();

        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            return await this.dbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await this.dbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return await this.dbContext.Products.Include(p => p.ProductCategory)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageURL = x.ImageURL,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CategoryId = x.ProductCategory.Id,
                    CategoryName = x.ProductCategory.Name
                }).ToListAsync();
        }
    }
}
