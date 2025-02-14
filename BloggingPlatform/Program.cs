using BloggingPlatform.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLServer");

builder.Services.AddControllers();
builder.Services.AddDbContext<BlogginPlatformDbContext>(options =>
{
    options.UseSqlServer(connectionString ?? "connection_string");
});
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

app.MapControllers();

app.Run();
