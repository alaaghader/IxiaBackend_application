using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService) 
        {
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Add Currency
        /// </summary>
        /// <param name="currencyName">Currency name</param>
        [HttpPost("AddCurrency")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCurrencyAsync(string currencyName)
        {
            var result = await currencyService.AddCurrencyAsync(currencyName);
            return result.ToActionResult();
        }
    }
}
