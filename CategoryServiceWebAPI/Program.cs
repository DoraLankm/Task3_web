using AutoMapper;
using CategoryService.Abstraction;
using CategoryService.Service;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Shared.Mapper;

namespace CategoryServiceWebAPI
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
            builder.Services.AddScoped<ICategoryServices, CategoryService.Service.CategoryService>();

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