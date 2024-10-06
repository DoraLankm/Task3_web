
using CategoryService.Abstraction;
using ProductServiceWebAPI.Abstraction;
using Shared.Dto;
using StorageServiceWebAPI.Abstraction;

namespace GraphQL_lesson.Query
{
    public class QueryClass
    {
        public IEnumerable <ProductDto> getProducts([Service] IProductServices service) => service.GetProducts();

        public IEnumerable<StorageDto> getStorages([Service] IStorageServices service) => service.GetStorages();

        public IEnumerable<CategoryDto> getCategories([Service] ICategoryServices service) => service.GetCategories();

    }
}
