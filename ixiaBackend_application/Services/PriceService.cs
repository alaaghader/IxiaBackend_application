﻿using AutoMapper;
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

        public async Task<Result<List<PriceView>>> GetPricesByCountry(string countryName)
        {
            var result = await (from prices in _ixiaContext.Prices
                                join countries in _ixiaContext.Countries
                                on prices.CountryId equals countries.Id
                                where countries.Name == countryName
                                select prices)
                               .ProjectTo<PriceView>(_mapper.ConfigurationProvider)
                               .ToListAsync();
            return result;
        }

        public async Task<Result<List<PriceView>>> SearchPricesByCountry(string countryName, string prodName)
        {
            var result = await(from prices in _ixiaContext.Prices
                               join countries in _ixiaContext.Countries
                               on prices.CountryId equals countries.Id
                               join products in _ixiaContext.Products
                               on prices.ProductId equals products.Id
                               where countries.Name == countryName 
                               && products.Name == prodName
                               select prices)
                               .ProjectTo<PriceView>(_mapper.ConfigurationProvider)
                               .ToListAsync();
            return result;
        }
    }
}