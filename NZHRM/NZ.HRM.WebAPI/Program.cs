using Microsoft.EntityFrameworkCore;
using NZ.HRM.Infrastructure.Persistence;
using NZ.HRM.Application.DependencyInjection;
using NZ.HRM.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Connection String in appsettings.json
// "DefaultConnection": "Host=localhost;Database=NZHRM_Db;Username=postgres;Password=yourpassword"

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    m => m.MigrationsAssembly("NZ.HRM.Infrastructure")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHandlerServices();
builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
