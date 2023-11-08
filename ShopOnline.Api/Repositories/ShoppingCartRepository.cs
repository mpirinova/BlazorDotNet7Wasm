using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.dbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in this.dbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAddDto.Quantity
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.dbContext.CartItems.AddAsync(item);
                    await this.dbContext.SaveChangesAsync();
                    return new CartItemDto
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        CartId = item.CartId,
                        ProductName = item.Product.Name,
                        ProductDescription = item.Product.Description,
                        ProductImageURL = item.Product.ImageURL,
                        Price = item.Product.Price,
                        TotalPrice = item.Product.Price * item.Quantity,
                    };
                }
            }

            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItemDto> GetItem(int id)
        {
            return await (from cart in this.dbContext.Carts
                          join cartItem in this.dbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItemDto
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                              ProductName = cartItem.Product.Name,
                              ProductDescription = cartItem.Product.Description,
                              ProductImageURL = cartItem.Product.ImageURL,
                              Price = cartItem.Product.Price,
                              TotalPrice = cartItem.Product.Price * cartItem.Quantity,
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            return await (from cart in this.dbContext.Carts
                          join cartItem in this.dbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItemDto
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                              ProductName = cartItem.Product.Name,
                              ProductDescription = cartItem.Product.Description,
                              ProductImageURL = cartItem.Product.ImageURL,
                              Price = cartItem.Product.Price,
                              TotalPrice = cartItem.Product.Price * cartItem.Quantity,
                          }).ToListAsync();
        }

        public Task<CartItem> UpdateQuantity(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
