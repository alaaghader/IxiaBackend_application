using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private ICountryService _countryService;

        public CountryController(ICountryService countryService) 
        {
            _countryService = countryService;
        }

        /// <summary>
        /// Add Country
        /// </summary>
        /// <param name="countryName">Country name</param>
        [HttpPost("AddCountry")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCountryAsync(string countryName)
        {
            var result = await _countryService.AddCountryAsync(countryName);
            return result.ToActionResult();
        }
    }
}
