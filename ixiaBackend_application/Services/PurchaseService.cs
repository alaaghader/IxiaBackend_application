using AutoMapper;
using AutoMapper.QueryableExtensions;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class PurchaseService : IPurchaseService
    {

        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<PurchaseView>>> GetAllPurchasesAsync(string userId)
        {
            var result = await(from purchases in _context.Purchases
                               join user in _context.Users
                               on purchases.UserId equals user.Id
                               where user.Id == userId
                               select _mapper.Map(purchases, new PurchaseView
                               {
                                   Product = _mapper.Map(purchases.Product, new ProductView {
                                       TotalFavorite = _context.Favorites.Select(x => x.ProductId == purchases.Product.Id).Count(),
                                       IsFavorite = userId != null && _context.Favorites
                                            .Any(x => x.UserId == userId && x.ProductId == purchases.Product.Id),
                                       Category = _mapper.Map(purchases.Product.Category, new CategoryView { }),
                                       Company = _mapper.Map(purchases.Product.Company, new CompanyView { }),
                                   }),
                               })).ToListAsync();
            return result;
        }

        public async Task<Result<bool>> AddPurchaseAsync(string userId, int productId, string comments)
        {
            var result = await _context.Purchases.SingleOrDefaultAsync(e => e.UserId == userId && e.ProductId == productId);
            if (result == null)
            {
                var newRecord = new Purchase
                {
                    UserId = userId,
                    ProductId = productId,
                    PurchaseTime = DateTime.Now,
                    Comments = comments,
                };
                await _context.Purchases.AddAsync(newRecord);
                await _context.SaveChangesAsync();
            }
            else
            {
                return Result.Conflict<bool>().With(Error.ProductAlreadyOrdered());
            }
            return true;
        }
    }
}
