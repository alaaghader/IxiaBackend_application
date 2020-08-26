using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompanyAsync(CompanyInput companyInput)
        {
            var result = await _companyService.AddCompanyAsync(companyInput);
            return result.ToActionResult();
        }

        [HttpGet("GetCompanies")]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            var result = await _companyService.GetCompaniesAsync();
            return result.ToActionResult();
        }

        [HttpGet("GetCompanyDetails/{id}")]
        public async Task<IActionResult> GetCompanyDetailsAsync(int id)
        {
            var result = await _companyService.GetCompanyDetails(id);
            return result.ToActionResult();
        }
    }
}
