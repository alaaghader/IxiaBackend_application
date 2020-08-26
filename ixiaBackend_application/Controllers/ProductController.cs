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

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductAsync(ProductInput productInput)
        {
            var result = await _productService.AddProductAsync(productInput);
            return result.ToActionResult();
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProductAsync()
        {
            var result = await _productService.GetAllProductsAsync();
            return result.ToActionResult();
        }

        [HttpGet("GetProductDetails/{id}")]
        public async Task<IActionResult> GetProductDetailsAsync(int id)
        {
            var result = await _productService.GetProductDetailsAsync(id);
            return result.ToActionResult();
        }
    }
}
