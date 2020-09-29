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
    public class FavoriteService : IFavoriteService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public FavoriteService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<FavoriteView>>> GetAllFavoritesAsync(string userId)
        {
            var result = await (from favorites in _context.Favorites
                                join user in _context.Users
                                on favorites.UserId equals user.Id
                                where user.Id == userId
                                select _mapper.Map(favorites, new FavoriteView
                                {
                                    Price = _mapper.Map(favorites, new PriceView
                                    {
                                        PriceNumber = _context.Prices.SingleOrDefault(x => x.ProductId == favorites.ProductId && 
                                                        x.CountryId == favorites.CountryId && x.CurrencyId == favorites.CurrencyId)
                                                        .PriceNumber,
                                        Product = _mapper.Map(favorites.Product, new ProductView {
                                            TotalFavorite = _context.Favorites.Select(x => x.ProductId == favorites.ProductId).Count(),
                                            IsFavorite = userId != null && _context.Favorites
                                                .Any(x => x.UserId == userId && x.ProductId == favorites.ProductId),
                                            Type = _mapper.Map(favorites.Product.Type, new TypeView { }),
                                            Company = _mapper.Map(favorites.Product.Company, new CompanyView { }),
                                        }),
                                        Country = _mapper.Map(favorites.Country, new CountryView { }),
                                        Currency = _mapper.Map(favorites.Currency, new CurrencyView { }),
                                    }),
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<bool>> ToggleProductFavoriteAsync(string userId, int productId, int countryId, int currencyId)
        {
            var result = await _context.Favorites.SingleOrDefaultAsync(e => e.UserId == userId && e.ProductId == productId);
            if(result == null)
            {
                var newRecord = new Favorite
                {
                    UserId = userId,
                    ProductId = productId,
                    CountryId = countryId,
                    CurrencyId = currencyId,
                    FavoriteTime = DateTime.Now,
                };
                await _context.Favorites.AddAsync(newRecord);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Favorites.Remove(result);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
