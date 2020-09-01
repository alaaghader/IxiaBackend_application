using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="productInput">Product input</param>
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductAsync(ProductInput productInput)
        {
            var result = await _productService.AddProductAsync(productInput);
            return result.ToActionResult();
        }

        /// <summary>
        /// Search Product
        /// </summary>
        /// <param name="name">Product name</param>
        /// <returns>Product Details</returns>
        [HttpPost("SearchProduct")]
        public async Task<IActionResult> SearchProductAsync(string name)
        {
            var result = await _productService.SearchProductsAsync(name);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns>All Product Details</returns>
        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var result = await _productService.GetAllProductsAsync();
            return result.ToActionResult();
        }

        /// <summary>
        /// Get Product Details
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product Details</returns>
        [HttpGet("GetProductDetails/{id}")]
        public async Task<IActionResult> GetProductDetailsAsync(int id)
        {
            var result = await _productService.GetProductDetailsAsync(id);
            return result.ToActionResult();
        }
    }
}
