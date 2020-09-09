using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly UserManager<User> userManager;

        public ProductController(IProductService productService,
            UserManager<User> userManager)
        {
            _productService = productService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="productInput">Product input</param>
        [HttpPost("AddProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProductAsync(ProductInput productInput)
        {
            var result = await _productService.AddProductAsync(productInput);
            return result.ToActionResult();
        }

        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="input">Product name</param>
        /// <returns>Product Details</returns>
        [HttpPost("SearchProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProductAsync(SearchInput input)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _productService.SearchProductsAsync(input.Name ,null);
                return result.ToActionResult();
            }
            else
            {
                var result = await _productService.SearchProductsAsync(input.Name, user.Id);
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns>All Product Details</returns>
        [HttpGet("GetAllProduct")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _productService.GetAllProductsAsync(null);
                return result.ToActionResult();
            }
            else
            {
                var result = await _productService.GetAllProductsAsync(user.Id);
                return result.ToActionResult();
            }
        }

        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product Details</returns>
        [HttpPost("GetProductDetails/{id}")]
        public async Task<IActionResult> GetProductDetailsAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                var result = await _productService.GetProductDetailsAsync(id, null);
                return result.ToActionResult();
            }
            else 
            {
                var result = await _productService.GetProductDetailsAsync(id, user.Id);
                return result.ToActionResult();
            }
        }
    }
}
