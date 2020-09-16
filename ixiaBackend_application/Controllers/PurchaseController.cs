using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly UserManager<User> userManager;

        public PurchaseController(IPurchaseService purchaseService,
            UserManager<User> userManager)
        {
            _purchaseService = purchaseService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get All Purchases Details
        /// </summary>
        /// <returns>All Purchases Details</returns>
        [HttpGet("GetAllPurchases")]
        [Authorize]
        public async Task<IActionResult> GetAllPurchasesAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _purchaseService.GetAllPurchasesAsync(user.Id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Toggle Purchase
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="countryId">Country id</param>
        /// <param name="currencyId">Currency id</param>
        /// <param name="comments">Comments</param>
        [HttpPost("AddPurchase/{productId}/{countryId}/{currencyId}")]
        [Authorize]
        public async Task<IActionResult> AddPurchasesAsync(int productId, int countryId, int currencyId, string comments)
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _purchaseService.AddPurchaseAsync(user.Id, productId, countryId, currencyId, comments);
            return result.ToActionResult();
        }
    }
}
