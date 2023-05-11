using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.ECommerce.Api.Middleware;
using Sample.ECommerce.Application;
using Sample.ECommerce.Domain;
using Sample.ECommerce.Domain.Entities;
using Sample.ECommerce.Domain.Interfaces;
using Sample.ECommerce.Persistence;
using Sample.ECommerce.Persistence.Context;
using Sample.ECommerce.Persistence.Repositories;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scoped = app.Services.CreateScope())
{
    try
    {
        var productRepository = scoped.ServiceProvider.GetRequiredService<IProductRepository>();
        var unitOfWork = scoped.ServiceProvider.GetRequiredService<IUnitOfWork>();
        List<Product> listProducts = new List<Product>(){
                new Product() { Id = new Guid("e99b963f-917b-4924-9338-11e64b4e31a5"), Name = "Laptop", Price = 15500, StockQuantity = 10},
                new Product() { Id = new Guid("fb64e26e-eea0-4c49-8e3d-91423131e680"), Name = "Klavye", Price = 1000, StockQuantity = 250 },
                new Product() { Id = new Guid("447a12ec-b8b6-4659-ba5a-7a44faa7c028"), Name = "Gömlek", Price = 320, StockQuantity = 48 },
                new Product() { Id = new Guid("06064dca-e94e-42b7-a122-1be0334d75cc"), Name = "Pantolon", Price = 300, StockQuantity = 65 },
                new Product() { Id = new Guid("9ff414b9-ac6b-4a87-8056-cd8aa90c0166"), Name = "Mont", Price = 400, StockQuantity = 30 },
                new Product() { Id = new Guid("a9f1e343-4bfd-45c9-abe2-b7a10112126d"), Name = "T-shirt", Price = 190, StockQuantity = 85 },
                new Product() { Id = new Guid("44893f54-6a46-48d0-bffd-cb5731aa054e"), Name = "Çorap", Price = 35, StockQuantity = 100 },
                new Product() { Id = new Guid("10d2b08e-c9cb-4f04-a0c6-34063686b88e"), Name = "Çiçek", Price = 70, StockQuantity = 150 },
                new Product() { Id = new Guid("c7b9a54e-f77d-423e-8ef2-5c3130a312c9"), Name = "Güç bandı", Price = 50, StockQuantity = 15 },
                new Product() { Id = new Guid("a4d35c34-e58c-462e-8bff-a26f22c7dcbe"), Name = "Dumbell", Price = 99, StockQuantity = 6 }
        };
        await productRepository!.AddRangeAsync(listProducts);
        await unitOfWork.SaveChangesAsync();
    }
    catch (Exception)
    {
    }
}
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
