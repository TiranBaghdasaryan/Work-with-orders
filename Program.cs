using Microsoft.EntityFrameworkCore;
using Work_with_orders.Commands.Executors;
using Work_with_orders.Context;
using Work_with_orders.Context.Seeds;
using Work_with_orders.DependencyInjection;
using Work_with_orders.Repositories;
using Work_with_orders.Repositories.BasketProductRepo;
using Work_with_orders.Repositories.OrderProductRepo;
using Work_with_orders.Services.Basket;
using Work_with_orders.Services.Order;
using Work_with_orders.Services.Product;
using Work_with_orders.Services.Token;
using AuthenticationService = Work_with_orders.Services.Authentication.AuthenticationService;
using IAuthenticationService = Work_with_orders.Services.Authentication.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidations();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<BasketRepository>();
builder.Services.AddScoped<BasketProductRepository>();
builder.Services.AddScoped<OrderProductRepository>();

// builder.Services.AddScoped<Command<IValidator<long>>, LongExecutor>();
builder.Services.AddScoped<IGetProductExecutor, GetProductExecutor>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnection")!);
});

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthenticationConfiguration();


var app = builder.Build();
await app.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();