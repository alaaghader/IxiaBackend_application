using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : Controller
    {
        private IPriceService _priceService;
        private readonly UserManager<User> userManager;

        public PriceController(IPriceService priceService,
            UserManager<User> userManager) 
        {
            _priceService = priceService;
            this.userManager = userManager;
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
        [HttpGet("GetPrices/{countryName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPriceAsync(string countryName)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.GetPricesByCountry(countryName,null );
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.GetPricesByCountry(countryName, user.Id);
                return result.ToActionResult();
            }
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
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.SearchPricesByCountry(countryName, prodName, null);
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.SearchPricesByCountry(countryName, prodName, user.Id);
                return result.ToActionResult();
            }
        }
    }
}
