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

// �������� ������ ����������� �� ������������
var connectionString = builder.Configuration.GetConnectionString("db");

// ������������ AppDbContext � �������������� DbContextOptions
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseLazyLoadingProxies().UseNpgsql(connectionString));

// ������������ �������, ������� ������� �� ��������� ��� Scoped
builder.Services.AddScoped<ICategoryServices, CategoryService.Service.CategoryService>();
builder.Services.AddScoped<IProductServices, ProductService>();
builder.Services.AddScoped<IStorageServices, StorageService>();

// ��������� ��������� AutoMapper � MemoryCache
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddMemoryCache();

// ��������� ������������ Ocelot �� ����� ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// �������� Ocelot � DI
builder.Services.AddOcelot();

// ��������� ��������� GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryClass>()
    .AddMutationType<MutationClass>()
    .AddTypeExtension<StorageMutation>();

// ��������� ��������� ������������
builder.Services.AddControllers();

var app = builder.Build();

// ��������� �������� ��� ������� ����������
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapGraphQL();
app.MapControllers();

// �������� ������������� Ocelot middleware
await app.UseOcelot();

app.Run();
