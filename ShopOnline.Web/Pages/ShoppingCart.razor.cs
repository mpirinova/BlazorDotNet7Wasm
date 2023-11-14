using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ShoppingCart
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        public List<CartItemDto> ShoppingCartItems { get; set; }

        public string ErrorMessage { get; set; }

        protected string TotalPrice { get; set; }

        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CartChanged();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(cartItemDto.Id);
            CartChanged();
        }

        private CartItemDto GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(i => i.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);
        }

        protected async Task UpdateQuantityCartItem(int id, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDto
                    {
                        CartItemId = id,
                        Quantity = quantity
                    };

                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQuantity(updateItemDto);

                    UpdateItemTotalPrice(returnedUpdateItemDto);
                    CartChanged();
                    await MakeUpdateQuantityButtonVisible(id, false);
                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);

                    if (item is not null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected async Task UpdateQuantity(int id)
        {
            await MakeUpdateQuantityButtonVisible(id, true);
        }

        private async Task MakeUpdateQuantityButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQuantityButtonVisible", id, visible);
        }

        private void UpdateItemTotalPrice(CartItemDto cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item is not null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Quantity;
            }

        }

        private void CartChanged()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Quantity);
            ShoppingCartService.RaiseEventOnCartQuantityChanged(TotalQuantity);
        }
    }
}
