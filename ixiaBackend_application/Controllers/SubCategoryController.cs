using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : Controller
    {
        private ISub_CategoriesService sub_CategoriesService;

        public SubCategoryController(ISub_CategoriesService sub_CategoriesService) 
        {
            this.sub_CategoriesService = sub_CategoriesService;
        }

        /// <summary>
        /// Get SubCategory
        /// </summary>
        /// <param name="id">category id</param>
        [HttpPost("getSubCategories/{id}")]
        public async Task<IActionResult> GetSubCategoryAsync(int id)
        {
            var result = await sub_CategoriesService.GetSubCategoryAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All SubCategories
        /// </summary>
        [HttpPost("getallsubcategories")]
        public async Task<IActionResult> GetAllSubCategoriesAsync()
        {
            var result = await sub_CategoriesService.GetAllSubCategoriesAsync();
            return result.ToActionResult();
        }
    }
}
