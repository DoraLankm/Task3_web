using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Mapper;
using GraphQL_lesson.Mutation;
using GraphQL_lesson.Mutatuin;
using GraphQL_lesson.Query;
using GraphQL_lesson.Service;

namespace GraphQL_lesson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // �������� ������ ����������� �� ������������
            var connectionString = builder.Configuration.GetConnectionString("db");

            // ������������ AppDbContext �������
            builder.Services.AddScoped<AppDbContext>(provider => new AppDbContext(connectionString));

            // ������������ �������, ������� ������� �� ��������� ��� Scoped
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<ICategoryServices, CategoryService>();
            builder.Services.AddScoped<IStorageServices, StorageService>();

            // ��������� ��������� AutoMapper � MemoryCache
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddMemoryCache();

            // ��������� ��������� GraphQL
            builder.Services.AddGraphQLServer().AddQueryType<QueryClass>().AddMutationType<MutationClass>();

            builder.Services.AddGraphQLServer().AddQueryType<QueryClass>().AddMutationType<StorageMutation>();

            var app = builder.Build();

            app.MapGraphQL();

            app.Run();
        }
    }
}
