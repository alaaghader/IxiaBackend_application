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
                                    Price = {
                                         Product = {
                                            TotalFavorite = _context.Favorites.Select(x => x.ProductId == favorites.ProductId).Count(),
                                            IsFavorite = userId != null && _context.Favorites
                                            .Any(x => x.UserId == userId && x.ProductId == favorites.ProductId),
                                            Category = _mapper.Map(favorites.Product.Category, new CategoryView { }),
                                            Company = _mapper.Map(favorites.Product.Company, new CompanyView { }),
                                         },
                                         //Currency = _mapper.Map(favorites.Currency, new CurrencyView { }),
                                        //Country = _mapper.Map(favorites.Country, new CountryView { }),
                                    },
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<bool>> ToggleProductFavoriteAsync(string userId, int productId)
        {
            var result = await _context.Favorites.SingleOrDefaultAsync(e => e.UserId == userId && e.ProductId == productId);
            if(result == null)
            {
                var newRecord = new Favorite
                {
                    UserId = userId,
                    ProductId = productId,
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
