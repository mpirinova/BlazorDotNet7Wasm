﻿using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDto>> GetItems(int userId);

        Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto);

        Task<CartItemDto> DeleteItem(int id);

        Task<CartItemDto> UpdateQuantity(CartItemQtyUpdateDto cartItemQtyUpdateDto);

        event Action<int> OnCartQuantityChanged;

        void RaiseEventOnCartQuantityChanged(int totalQuantity);
    }
}