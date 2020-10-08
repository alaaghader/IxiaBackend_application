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
    public class PriceService : IPriceService
    {
        private IxiaContext _ixiaContext;
        private IMapper _mapper;

        public PriceService(IxiaContext ixiaContext,
            IMapper mapper)
        {
            _ixiaContext = ixiaContext;
            _mapper = mapper;
        }

        public async Task<Result<PriceView>> AddPriceAsync(int prodiId, int countryId, int currencyId, double price)
        {
            var inTable = await _ixiaContext.Prices.SingleOrDefaultAsync(e => e.ProductId == prodiId
                                            && e.CountryId == countryId && e.CurrencyId == countryId);
            if (inTable != null)
                return Result.Conflict<PriceView>().With(Error.PriceAlreadyExists());
            else 
            {
                var newRecord = new Price {
                    CountryId = countryId,
                    CurrencyId = currencyId,
                    ProductId = prodiId,
                    PriceNumber = price,
                };

                await _ixiaContext.Prices.AddAsync(newRecord);
                await _ixiaContext.SaveChangesAsync();

                return _mapper.Map(newRecord, new PriceView { });
            }
        }

        public async Task<Result<List<PriceView>>> GetPricesByCountry(string countryName, string userId)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                where countries.Name == countryName
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                        .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView { 
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<List<PriceView>>> GetPricesByCountryAndCategory(string countryName, string userId, int categoryId)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                where countries.Name == countryName && prices.Product.Type.Sub_Category.Category.Id == categoryId
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                        .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView
                                        {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView
                                            {
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<List<PriceView>>> GetPricesByCountryAndSubCategory(string countryName, string userId, int subCategoryId)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                where countries.Name == countryName && prices.Product.Type.Sub_Category.Id == subCategoryId
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                        .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView
                                        {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView
                                            {
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<List<PriceView>>> SearchPricesByCountry(string countryName, string prodName, string userId)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                join products in _ixiaContext.Products
                                on prices.ProductId equals products.Id
                                where countries.Name == countryName
                                && products.Name.Contains(prodName) 
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                         .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView
                                        {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView
                                            {
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).ToListAsync();
            return result;
        }

        public async Task<Result<PriceView>> GetProductDetailsByCountryAsync(int id, string userId, string countryName)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                join products in _ixiaContext.Products
                                on prices.ProductId equals products.Id
                                where countries.Name == countryName
                                && products.Id == id
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                         .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView
                                        {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView
                                            {
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).SingleAsync();
            return result;
        }

        public async Task<Result<List<PriceView>>> GetRecommendedProducts(int prodId, string userId, string countryName) 
        {
            var record = await _ixiaContext.Products.SingleOrDefaultAsync(e => e.Id == prodId);
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                join prodcuts in _ixiaContext.Products
                                on prices.ProductId equals prodcuts.Id
                                where record.TypeId == prodcuts.TypeId
                                && countries.Name == countryName
                                && prodcuts.Id != prodId
                                select _mapper.Map(prices, new PriceView
                                {
                                    Product = _mapper.Map(prices.Product, new ProductView
                                    {
                                        TotalFavorite = _ixiaContext.Favorites.Select(x => x.ProductId == prices.ProductId).Count(),
                                        IsFavorite = userId != null && _ixiaContext.Favorites
                                         .Any(x => x.UserId == userId && x.ProductId == prices.ProductId),
                                        Type = _mapper.Map(prices.Product.Type, new TypeView
                                        {
                                            Sub_Category = _mapper.Map(prices.Product.Type.Sub_Category, new Sub_CategoryView
                                            {
                                                Category = _mapper.Map(prices.Product.Type.Sub_Category.Category, new CategoryView { }),
                                            }),
                                        }),
                                        Company = _mapper.Map(prices.Product.Company, new CompanyView { }),
                                    }),
                                    Currency = _mapper.Map(prices.Currency, new CurrencyView { }),
                                    Country = _mapper.Map(prices.Country, new CountryView { }),
                                })).ToListAsync();

            return result;
        }
    }
}
