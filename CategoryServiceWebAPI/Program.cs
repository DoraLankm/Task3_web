using AutoMapper;
using CategoryService.Abstraction;
using CategoryService.Service;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Shared.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("db");

// Регистрируем AppDbContext с использованием DbContextOptions
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

// Регистрируем сервисы
builder.Services.AddScoped<ICategoryServices, CategoryService.Service.CategoryService>();

// Регистрируем AutoMapper с общим профилем
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Добавляем поддержку MemoryCache
builder.Services.AddMemoryCache();

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Устанавливаем порт для приложения
builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

// Применяем миграции при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
