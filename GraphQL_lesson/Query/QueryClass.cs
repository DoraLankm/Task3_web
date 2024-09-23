using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Query
{
    public class QueryClass
    {
        public IEnumerable <ProductDto> GetProducts([Service] IProductServices service) => service.GetProducts();

        public IEnumerable<StorageDto> GetStorages([Service] IStorageServices service) => service.GetStorages();

        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryServices service) => service.GetCategories();

    }
}
