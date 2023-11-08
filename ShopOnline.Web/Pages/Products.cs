using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public partial class Products
    {
        [Inject]
        public IProductsService ProductsService { get; set; }

        public IEnumerable<ProductDto> ProductsList { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ProductsList = await ProductsService.GetProducts();
        }

        protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
        {
            return from product in ProductsList
                   group product by product.CategoryId into prodByCatGroup
                   orderby prodByCatGroup.Key
                   select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int, ProductDto> groupedProducts)
        {
            return groupedProducts.FirstOrDefault(pg => pg.CategoryId == groupedProducts.Key).CategoryName;
        }
    }
}
