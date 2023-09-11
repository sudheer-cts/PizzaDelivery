using ePizzaHub.Entities;
using ePizzaHub.Repositories.Implementations;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Repositories;
using ePizzaHub.Services.Configuration;
using ePizzaHub.WebUI.Configuration;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using ePizzaHub.Services.Implementations;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Helpers;
using ePizzaHub.WebUI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, Role>().
               AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserAccessor, UserAccessor>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddTransient<IRepository<Item>, Repository<Item>>();
builder.Services.AddTransient<IRepository<Category>, Repository<Category>>();
builder.Services.AddTransient<IRepository<ItemType>, Repository<ItemType>>();
builder.Services.AddTransient<IRepository<CartItem>, Repository<CartItem>>();
builder.Services.AddTransient<IRepository<OrderItem>, Repository<OrderItem>>();
builder.Services.AddTransient<IRepository<PaymentDetails>, Repository<PaymentDetails>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
