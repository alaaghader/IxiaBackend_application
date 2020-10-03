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
        /// Get Type
        /// </summary>
        /// <param name="id">sub category id</param>
        [HttpPost("getSubCategories/{id}")]
        public async Task<IActionResult> GetTypeAsync(int id)
        {
            var result = await typeService.GetTypeAsync(id);
            return result.ToActionResult();
        }


        /// <summary>
        /// Get All Types
        /// </summary>
        [HttpPost("getalltypes")]
        public async Task<IActionResult> GetAllTypesAsync ()
        {
            var result = await typeService.GetAllTypesAsync();
            return result.ToActionResult();
        }
    }
}
