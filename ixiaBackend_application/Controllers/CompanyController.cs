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
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// Add Company
        /// </summary>
        /// <param name="companyInput">Company input</param>
        [HttpPost("AddCompany")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCompanyAsync(CompanyInput companyInput)
        {
            var result = await _companyService.AddCompanyAsync(companyInput);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All companies
        /// </summary>
        /// <returns>All Companies Details</returns>
        [HttpGet("GetCompanies")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCompaniesAsync()
        {
            var result = await _companyService.GetCompaniesAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Company Details
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Company Details</returns>
        [HttpGet("GetCompanyDetails/{id}")]
        public async Task<IActionResult> GetCompanyDetailsAsync(int id)
        {
            var result = await _companyService.GetCompanyDetails(id);
            return result.ToActionResult();
        }
    }
}
