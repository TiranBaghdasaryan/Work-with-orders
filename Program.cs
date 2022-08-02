using Microsoft.EntityFrameworkCore;
using Work_with_orders.Context;
using Work_with_orders.Context.Seeds;
using Work_with_orders.DependencyInjection;
using Work_with_orders.Repositories;
using Work_with_orders.Services.Token;
using AuthenticationService = Work_with_orders.Services.Authentication.AuthenticationService;
using IAuthenticationService = Work_with_orders.Services.Authentication.IAuthenticationService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnectionLocal") ??
                      throw new InvalidOperationException("Database Connection Fail"));
});

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();
await app.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();