using Microsoft.EntityFrameworkCore;
using ShopOnline.Server.Data;
using ShopOnline.Server.Entities;
using ShopOnline.Server.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Server.Repositories
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

                if (item is not null)
                {
                    var result = await this.dbContext.CartItems.AddAsync(item);
                    await this.dbContext.SaveChangesAsync();
                    var product = await this.dbContext.Products.FindAsync(result.Entity.ProductId);
                    return new CartItemDto
                    {
                        Id = result.Entity.Id,
                        ProductId = result.Entity.ProductId,
                        Quantity = result.Entity.Quantity,
                        CartId = result.Entity.CartId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        TotalPrice = product.Price * result.Entity.Quantity,
                    };
                }
            }

            return null;
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            var item = await this.dbContext.CartItems.Include(x => x.Product).Where(x => x.Id == id).SingleOrDefaultAsync();

            if (item is not null)
            {
                this.dbContext.CartItems.Remove(item);
                await this.dbContext.SaveChangesAsync();
            }

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

        public async Task<CartItemDto> UpdateQuantity(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.dbContext.CartItems.FindAsync(id);

            if (item is not null)
            {
                item.Quantity = cartItemQtyUpdateDto.Quantity;
                await this.dbContext.SaveChangesAsync();
                var product = await this.dbContext.Products.FindAsync(item.ProductId);

                return new CartItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    CartId = item.CartId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductImageURL = product.ImageURL,
                    Price = product.Price,
                    TotalPrice = product.Price * item.Quantity,
                };
            }

            return null;
        }
    }
}
