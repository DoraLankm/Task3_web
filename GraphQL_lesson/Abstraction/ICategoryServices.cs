using GraphQL_lesson.Models.Dto;

namespace GraphQL_lesson.Abstraction
{
    public interface ICategoryServices
    {
        IEnumerable<CategoryDto> GetCategories();

        int AddCategory(CategoryDto category);
    }
}

