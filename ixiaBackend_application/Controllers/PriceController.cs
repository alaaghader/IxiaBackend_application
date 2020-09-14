using ixiaBackend_application.Helpers;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : Controller
    {
        private IPriceService _priceService;

        public PriceController(IPriceService priceService) 
        {
            _priceService = priceService;
        }

        /// <summary>
        /// Add Price
        /// </summary>
        /// <param name="productInput">Price input</param>
        [HttpPost("AddPrice")]
        [AllowAnonymous]
        public async Task<IActionResult> AddPriceAsync(PriceInput productInput)
        {
            var result = await _priceService.AddPriceAsync(productInput.ProductId, productInput.CountryId, 
                productInput.CurrencyId, productInput.Price);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Price By User Country
        /// </summary>
        /// <param name="countryName">Country name</param>
        [HttpGet("GetPrices")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPriceAsync(string countryName)
        {
            var result = await _priceService.GetPricesByCountry(countryName);
            return result.ToActionResult();
        }

        /// <summary>
        /// Search Price By User Country
        /// </summary>
        /// <param name="countryName">Country name</param>
        /// /// <param name="prodName">Product name</param>
        [HttpPost("SearchPrices")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchPriceAsync(string countryName, string prodName)
        {
            var result = await _priceService.SearchPricesByCountry(countryName, prodName);
            return result.ToActionResult();
        }
    }
}
