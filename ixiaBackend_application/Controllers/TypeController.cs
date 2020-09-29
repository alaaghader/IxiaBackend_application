using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : Controller
    {
        private ITypeService typeService;

        public TypeController(ITypeService typeService) 
        {
            this.typeService = typeService;
        }

        /// <summary>
        /// Get Types
        /// </summary>
        /// <param name="id">sub category id</param>
        [HttpPost("getSubCategories/{id}")]
        public async Task<IActionResult> GetTypeAsync(int id)
        {
            var result = await typeService.GetTypeAsync(id);
            return result.ToActionResult();
        }
    }
}
