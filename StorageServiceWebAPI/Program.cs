using AutoMapper;
using DataBase;
using Microsoft.EntityFrameworkCore;
using StorageServiceWebAPI.Abstraction;
using StorageServiceWebAPI.Service;
using Shared.Mapper;
using Microsoft.Extensions.Caching.Memory;

namespace StorageServiceWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ������������
            var connectionString = builder.Configuration.GetConnectionString("db");

            // ������������ AppDbContext � �������������� DbContextOptions
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(connectionString));

            // ������������ �������
            builder.Services.AddScoped<IStorageServices, StorageService>();

            // ������������ AutoMapper � ����� ��������
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            // ��������� ��������� MemoryCache
            builder.Services.AddMemoryCache();

            // ��������� ��������� ������������
            builder.Services.AddControllers();

            // ��������� Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ��������� �������� ��� ������� ����������
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            
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
