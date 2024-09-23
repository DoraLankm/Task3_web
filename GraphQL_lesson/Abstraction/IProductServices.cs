using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Abstraction
{
    public interface IProductServices
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
    }
}
