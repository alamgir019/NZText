using Microsoft.EntityFrameworkCore;
using NZ.HRM.Infrastructure.Persistence;
using NZ.HRM.Application.DependencyInjection;
using NZ.HRM.Infrastructure.DependencyInjection;
using NZ.HRM.Application.Services;
using NZ.HRM.WebAPI.Services;
using NZ.Attendance.Infrastructure.DependencyInjection;
using NZ.Leave.Infrastructure.DependencyInjection;
using NZ.Payroll.Infrastructure.DependencyInjection;
using NZ.HRM.Domain.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    m => m.MigrationsAssembly("NZ.HRM.Infrastructure")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── HRM / Recruitment Module ───────────────────────────────────────────────
builder.Services.AddHandlerServices();
builder.Services.AddRepositories();
builder.Services.AddScoped<IEmployeeExcelExportService, EmployeeExcelExportService>();

// File storage
var fileStorageConfig = new FileStorageConfiguration();
builder.Configuration.GetSection("FileStorage").Bind(fileStorageConfig);
builder.Services.AddSingleton(fileStorageConfig);
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddHttpContextAccessor();

// Fingerprint device
var fingerprintConfig = new FingerprintDeviceConfiguration();
builder.Configuration.GetSection("FingerprintDevice").Bind(fingerprintConfig);
builder.Services.AddSingleton(fingerprintConfig);
builder.Services.AddHttpClient<IFingerprintDeviceService, FingerprintDeviceService>();

// ── Feature Modules ────────────────────────────────────────────────────────
builder.Services.AddAttendanceModule(builder.Configuration);
builder.Services.AddLeaveModule(builder.Configuration);
builder.Services.AddPayrollModule(builder.Configuration);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSession();


app.Run();
