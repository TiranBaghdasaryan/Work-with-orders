using Microsoft.EntityFrameworkCore;
using Work_with_orders.Commands.Executors.AdminExecutor;
using Work_with_orders.Commands.Executors.AdminExecutor.BlockUser;
using Work_with_orders.Commands.Executors.AdminExecutor.UnblockUser;
using Work_with_orders.Commands.Executors.ProductExecutors.CreateProduct;
using Work_with_orders.Commands.Executors.ProductExecutors.GetProduct;
using Work_with_orders.Commands.Executors.ProductExecutors.UpdateProduct;
using Work_with_orders.Context;
using Work_with_orders.Context.Seeds;
using Work_with_orders.DependencyInjection;
using Work_with_orders.Filters;
using Work_with_orders.Services.Token;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddValidations();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors();


builder.Services.AddDbContext<ApplicationContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("postgresConnection")!);
    },
    ServiceLifetime.Transient);

builder.Services.AddRepositories();
builder.Services.AddServices();



builder.Services.AddScoped<IGetProductExecutor, GetProductExecutor>();
builder.Services.AddScoped<ICreateProductExecutor, CreateProductExecutor>();
builder.Services.AddScoped<IUpdateProductExecutor, UpdateProductExecutor>();

builder.Services.AddScoped<IFillUpUserBalanceExecutor, FillUpUserBalanceExecutor>();
builder.Services.AddScoped<IBlockUserExecutor, BlockUserExecutor>();
builder.Services.AddScoped<IUnblockUserExecutor, UnblockUserExecutor>();




builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthenticationConfiguration();

builder.Services.AddControllers(options =>
    options.Filters.Add(new CheckUserActivityFilter()));

var app = builder.Build();
await app.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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