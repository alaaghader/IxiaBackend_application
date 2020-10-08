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
        public async Task<IActionResult> GetPricesByCountry(string countryName)
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
        /// Get Price By User Country And Category
        /// </summary>
        /// <param name="countryName">Country name</param>
        /// <param name="categoryId">Country name</param>
        [HttpGet("GetPricesByCategory/{countryName}/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPricesByCountryAndCategory(string countryName, int categoryId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.GetPricesByCountryAndCategory(countryName, null, categoryId);
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.GetPricesByCountryAndCategory(countryName, user.Id, categoryId);
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Get Price By User Country And SubCategory
        /// </summary>
        /// <param name="countryName">Country name</param>
        /// <param name="subCategoryId">Sub category name</param>
        [HttpGet("GetPricesBySubCategory/{countryName}/{subCategoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPricesByCountryAndSubCategory(string countryName, int subCategoryId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.GetPricesByCountryAndSubCategory(countryName, null, subCategoryId);
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.GetPricesByCountryAndSubCategory(countryName, user.Id, subCategoryId);
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Get Recommended
        /// </summary>
        /// <param name="prodId">Prod id</param>
        /// <param name="countryName">Country name</param>
        [HttpGet("GetRecommended/{prodId}/{countryName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRecommended(string countryName, int prodId)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.GetRecommendedProducts(prodId, null, countryName);
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.GetRecommendedProducts(prodId, user.Id, countryName);
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Search Price By User Country
        /// </summary>
        /// <param name="countryName">Country name</param>
        /// /// <param name="prodName">Product name</param>
        [HttpPost("SearchPrices/{countryName}/{prodName}")]
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

        /// <summary>
        /// Get Product Details Price By User Country
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="countryName">Country name</param>
        [HttpPost("GetProductDetailsPrice/{id}/{countryName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetailsPriceAsync(int id, string countryName)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _priceService.GetProductDetailsByCountryAsync(id, null, countryName);
                return result.ToActionResult();
            }
            else
            {
                var result = await _priceService.GetProductDetailsByCountryAsync(id, user.Id, countryName);
                return result.ToActionResult();
            }
        }
    }
}
