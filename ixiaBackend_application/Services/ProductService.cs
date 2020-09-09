using AutoMapper;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ixiaBackend_application.Services
{
    public class ProductService : IProductService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public ProductService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<bool>> AddProductAsync(ProductInput productInput)
        {
            Product prod = new Product
            {
                CategoryId = productInput.CategoryId,
                Name = productInput.Name,
                Description = productInput.Description,
                ImageUrl = productInput.ImageUrl,
                Price = productInput.Price,
                CompanyId = productInput.CompanyId,
            };

            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<List<ProductView>>> GetAllProductsAsync(string userId)
        {
            var result = await (from product in _context.Products
                                select _mapper.Map(product, new ProductView
                                {
                                    TotalFavorite = _context.Favorites.Select(x => x.ProductId == product.Id).Count(),
                                    IsFavorite = userId != null && _context.Favorites
                                    .Any(x => x.UserId == userId && x.ProductId == product.Id),
                                    Category = _mapper.Map(product.Category, new CategoryView { }),
                                    Company = _mapper.Map(product.Company, new CompanyView { }),
                                })).ToListAsync();

            return result;
        }

        public async Task<Result<List<ProductView>>> SearchProductsAsync(string name, string userId)
        {
            var result = await (from product in _context.Products
                                where product.Name.Contains(name)
                                select _mapper.Map(product, new ProductView
                                {
                                    TotalFavorite = _context.Favorites.Select(x => x.ProductId == product.Id).Count(),
                                    IsFavorite = userId != null && _context.Favorites
                                    .Any(x => x.UserId == userId && x.ProductId == product.Id),
                                    Category = _mapper.Map(product.Category, new CategoryView { }),
                                    Company = _mapper.Map(product.Company, new CompanyView { }),
                                })).ToListAsync();

            return result;
        }

        public async Task<Result<ProductView>> GetProductDetailsAsync(int id, string userId)
        {
            var result = await (from product in _context.Products
                                where product.Id == id
                                select _mapper.Map(product, new ProductView {
                                    IsFavorite = userId != null && _context.Favorites
                                    .Any(x => x.UserId == userId && x.ProductId == product.Id),
                                    Category = _mapper.Map(product.Category, new CategoryView { }),
                                    Company = _mapper.Map(product.Company, new CompanyView { }),
                                }))
                                .FirstAsync();

            return result;
        }
    }
}
