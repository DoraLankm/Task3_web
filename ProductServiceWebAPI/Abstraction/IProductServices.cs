

using Shared.Dto;

namespace ProductServiceWebAPI.Abstraction
{
    public interface IProductServices
    {
        IEnumerable<ProductDto> GetProducts();
        int AddProduct(ProductDto product);
    }
}
