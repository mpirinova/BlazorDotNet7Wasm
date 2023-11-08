using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ProductDetails
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductsService ProductService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public ProductDto Product { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Product = await ProductService.GetProduct(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
