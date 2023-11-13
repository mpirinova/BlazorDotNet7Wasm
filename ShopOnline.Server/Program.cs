using Microsoft.EntityFrameworkCore;
using ShopOnline.Server.Data;
using ShopOnline.Server.Repositories;
using ShopOnline.Server.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<ShopOnlineDbContext>(options =>
    options.UseSqlServer("DefaultConnection"));

builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

app.Run();
