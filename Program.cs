using Work_with_orders.Context.Seeds;
using Work_with_orders.DependencyInjection;
using Work_with_orders.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddCors();

builder.Services.AddDatabase();
builder.Services.AddRepositories();
builder.Services.AddValidations();
builder.Services.AddServices();
builder.Services.AddExecutors();

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