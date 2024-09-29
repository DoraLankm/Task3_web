using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Mapper;
using GraphQL_lesson.Mutation;
using GraphQL_lesson.Mutatuin;
using GraphQL_lesson.Query;
using GraphQL_lesson.Service;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GraphQL_lesson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Получаем строку подключения из конфигурации
            var connectionString = builder.Configuration.GetConnectionString("db");

            // Регистрируем AppDbContext вручную
            builder.Services.AddScoped<AppDbContext>(provider => new AppDbContext(connectionString));

            // Регистрируем сервисы, которые зависят от контекста как Scoped
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<ICategoryServices, CategoryService>();
            builder.Services.AddScoped<IStorageServices, StorageService>();

            // Добавляем поддержку AutoMapper и MemoryCache
            builder.Services.AddAutoMapper(typeof(MapperProfile));
            builder.Services.AddMemoryCache();

            // Добавляем поддержку GraphQL
            builder.Services.AddGraphQLServer().AddQueryType<QueryClass>().AddMutationType<MutationClass>().AddMutationType<StorageMutation>();

            // Добавляем поддержку контроллеров
            builder.Services.AddControllers();

            // Загружаем конфигурацию Ocelot из файла ocelot.json
            //builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            // Добавляем Ocelot в DI
            //builder.Services.AddOcelot();

            var app = builder.Build();

            app.MapGraphQL();

            // Включаем использование Ocelot middleware
            //app.UseOcelot();

            app.Run();
        }
    }
}
