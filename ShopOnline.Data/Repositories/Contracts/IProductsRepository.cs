﻿using ShopOnline.Data.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Data.Repositories.Contracts
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