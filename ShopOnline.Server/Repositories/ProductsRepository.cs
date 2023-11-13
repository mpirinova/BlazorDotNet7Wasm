using Microsoft.EntityFrameworkCore;
using ShopOnline.Server.Data;
using ShopOnline.Server.Entities;
using ShopOnline.Server.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Server.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ProductsRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetCategories()
        {
            return await this.dbContext.ProductCategories
                .Select(x => new ProductCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Icon = x.Icon,
                })
                .ToListAsync();
        }

        public async Task<ProductCategoryDto> GetCategory(int id)
        {
            return await this.dbContext.ProductCategories
                .Select(x => new ProductCategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Icon = x.Icon,
                })
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            return await this.dbContext.Products
                .Include(p => p.ProductCategory)
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
                })
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

        public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int id)
        {
            return await (from p in this.dbContext.Products.Include(p => p.ProductCategory)
                          where p.CategoryId == id
                          select new ProductDto
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Description = p.Description,
                              ImageURL = p.ImageURL,
                              Price = p.Price,
                              Quantity = p.Quantity,
                              CategoryId = p.ProductCategory.Id,
                              CategoryName = p.ProductCategory.Name
                          }).ToListAsync();
        }
    }
}
