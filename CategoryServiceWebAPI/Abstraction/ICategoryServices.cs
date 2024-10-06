


using Shared.Dto;

namespace CategoryService.Abstraction
{
    public interface ICategoryServices
    {
        IEnumerable<CategoryDto> GetCategories();

        int AddCategory(CategoryDto category);
    }
}

