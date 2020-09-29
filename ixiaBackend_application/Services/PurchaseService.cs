using AutoMapper;
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
            var result = await (from purchases in _context.Purchases
                                join user in _context.Users
                                on purchases.UserId equals user.Id
                                where user.Id == userId
                                select _mapper.Map(purchases, new PurchaseView {
                                    Price = _mapper.Map(purchases, new PriceView
                                    {
                                        PriceNumber = _context.Prices.SingleOrDefault(x => x.ProductId == purchases.ProductId &&
                                                        x.CountryId == purchases.CountryId && x.CurrencyId == purchases.CurrencyId)
                                                        .PriceNumber,
                                        Product = _mapper.Map(purchases.Product, new ProductView {
                                            TotalFavorite = _context.Favorites.Select(x => x.ProductId == purchases.ProductId).Count(),
                                            IsFavorite = userId != null && _context.Favorites
                                                .Any(x => x.UserId == userId && x.ProductId == purchases.ProductId),
                                            Type = _mapper.Map(purchases.Product.Type, new TypeView { }),
                                            Company = _mapper.Map(purchases.Product.Company, new CompanyView { }),
                                        }),
                                        Country = _mapper.Map(purchases.Country, new CountryView { }),
                                        Currency = _mapper.Map(purchases.Currency, new CurrencyView { }),
                                    }),
                               })).ToListAsync();
            return result;
        }

        public async Task<Result<bool>> AddPurchaseAsync(string userId, int productId, int countryId, int currencyId, string comments)
        {
            var newRecord = new Purchase
            {
                UserId = userId,
                ProductId = productId,
                CountryId = countryId,
                CurrencyId = currencyId,
                PurchaseTime = DateTime.Now,
                Comments = comments,
            };
            await _context.Purchases.AddAsync(newRecord);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
