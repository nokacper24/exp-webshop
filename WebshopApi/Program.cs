using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebshopApi.Data;
using WebshopApi.Models;
using WebshopApi.Repos;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = String.Empty;
if (builder.Environment.IsDevelopment())
{
    connectionString = config["mySqlConnectionString"];
}
else
{
    connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
}
ArgumentException.ThrowIfNullOrEmpty(connectionString);

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseMySQL(connectionString));
builder.Services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddApiEndpoints();


// Add authentication and authorization
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();

// Add repository services
builder.Services.AddScoped<ProductsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<AppUser>();

app.MapControllers();

app.Run();
