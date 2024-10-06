using AutoMapper;
using DataBase;
using Microsoft.EntityFrameworkCore;
using ProductServiceWebAPI.Abstraction;
using ProductServiceWebAPI.Service;
using Shared.Mapper;
using Microsoft.Extensions.Caching.Memory;

namespace ProductServiceWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Получаем строку подключения из конфигурации
            var connectionString = builder.Configuration.GetConnectionString("db");

            // Регистрируем AppDbContext с использованием DbContextOptions
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(connectionString));

            // Регистрируем сервисы
            builder.Services.AddScoped<IProductServices, ProductService>();

            // Регистрируем AutoMapper с общим профилем
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            // Добавляем поддержку MemoryCache
            builder.Services.AddMemoryCache();

            // Добавляем поддержку контроллеров
            builder.Services.AddControllers();

            // Добавляем Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Применяем миграции при запуске приложения
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

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
        }
    }
}
