using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService; 
        }

        /// <summary>
        /// Get All Purchases Details
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>All Purchases Details</returns>
        [HttpGet("GetAllPurchases/{userId}")]
        public async Task<IActionResult> GetAllPurchasesAsync(string userId)
        {
            var result = await _purchaseService.GetAllPurchasesAsync(userId);
            return result.ToActionResult();
        }

        /// <summary>
        /// Toggle Purchase
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="productId">Product id</param>
        /// <param name="comments">Comments</param>
        [HttpPost("TogglePurchase")]
        public async Task<IActionResult> TogglePurchasesAsync(string userId, int productId, string comments)
        {
            var result = await _purchaseService.TooglePurchaseAsync(userId, productId, comments);
            return result.ToActionResult();
        }
    }
}
