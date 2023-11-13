using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class ProductsByCategory
    {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        public IProductsService ProductsService { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        public string CategoryName { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Products = await ProductsService.GetProductsByCategory(CategoryId);

                if (Products is not null && Products.Count() > 0)
                {
                    var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);

                    if (productDto is not null)
                    {
                        CategoryName = productDto.CategoryName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
