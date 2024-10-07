using CategoryService.Abstraction;
using DataBase;
using GraphQL_lesson.Mutation;
using GraphQL_lesson.Query;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using ProductServiceWebAPI.Abstraction;
using ProductServiceWebAPI.Service;
using Shared.Mapper;
using StorageServiceWebAPI.Abstraction;
using StorageServiceWebAPI.Service;
using Microsoft.EntityFrameworkCore;
using GraphQL_lesson.Mutatuin;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("db");

// Регистрируем AppDbContext с использованием DbContextOptions
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

// Регистрируем сервисы, которые зависят от контекста как Scoped
builder.Services.AddScoped<ICategoryServices, CategoryService.Service.CategoryService>();
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IStorageServices, StorageService>();

// Добавляем поддержку AutoMapper и MemoryCache
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddMemoryCache();

// Загружаем конфигурацию Ocelot из файла ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Добавляем Ocelot в DI
builder.Services.AddOcelot();

// Добавляем поддержку GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryClass>()
    .AddMutationType<MutationClass>()
    .AddTypeExtension<StorageMutation>();

// Добавляем поддержку контроллеров
builder.Services.AddControllers();

// Устанавливаем порт для приложения
builder.WebHost.UseUrls("http://*:5000");

var app = builder.Build();

// Применяем миграции при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Маршрутизация GraphQL и контроллеров
app.MapGraphQL();
app.MapControllers();

// Используем Ocelot middleware
await app.UseOcelot();

app.Run();
