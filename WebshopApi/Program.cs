using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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



// Following microsoft learn page on securing web APIs for SPAs
// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-8.0

// add identiy services
builder.Services.AddAuthorization();

// activate identity APIs
builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

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

// map identity routes
app.MapIdentityApi<AppUser>();
// add logout endpoint
app.MapPost("/logout",
    async (SignInManager<AppUser> signInManager, [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }
    return Results.Unauthorized();
})
.WithOpenApi()
.RequireAuthorization();

app.MapControllers();

app.Run();
