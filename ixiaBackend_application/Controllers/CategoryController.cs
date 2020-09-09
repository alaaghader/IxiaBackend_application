using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="categoryInput">Category input</param>
        [HttpPost("AddCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCategoryAsync(CategoryInput categoryInput)
        {
            var result = await _categoryService.AddCategoryAsync(categoryInput);
            return result.ToActionResult();
        }
    }
}
