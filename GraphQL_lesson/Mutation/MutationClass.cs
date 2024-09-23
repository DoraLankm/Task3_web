using GraphQL_lesson.Abstraction;
using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Mutatuin
{
    public class MutationClass
    {
        public int AddProduct(ProductDto product,[Service] IProductServices service)
        {
            var id = service.AddProduct(product);
            return id;
        }
    }
}
