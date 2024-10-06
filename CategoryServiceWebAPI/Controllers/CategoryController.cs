
using CategoryService.Abstraction;
using Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CategoryServiceWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;

        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        // Получить все категории
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetCategories()
        {
            var categories = _categoryService.GetCategories(); // Получаем список категорий
            return Ok(categories);
        }

        // Добавить новую категорию
        [HttpPost]
        public ActionResult<int> AddCategory([FromBody] CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Category cannot be null");
            }

            var categoryId = _categoryService.AddCategory(categoryDto); // Добавляем новую категорию
            return CreatedAtAction(nameof(GetCategories), new { id = categoryId }, categoryId); // Возвращаем ID новой категории
        }
    }
}
