using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebshopApi.Data;
using WebshopApi.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    connection = builder.Configuration["mySqlConnectionString"];
}
else
{
    connection = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");
}

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    _ = options.UseMySQL(connection);
}

);
builder.Services.AddScoped<ProductsRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapControllers();

app.Run();
