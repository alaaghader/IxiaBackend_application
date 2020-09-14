using AutoMapper;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public CurrencyService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CurrencyView>> AddCurrencyAsync(string currencyName)
        {
            var inTable = await _context.Currencies.SingleOrDefaultAsync(e => e.CurrencyName == currencyName);
            if (inTable != null)
                return Result.Conflict<CurrencyView>().With(Error.CurrencyAlreadyExists());
            else 
            {
                var newRecord = new Currency
                {
                    CurrencyName = currencyName,
                };

                await _context.Currencies.AddAsync(newRecord);
                await _context.SaveChangesAsync();

                return _mapper.Map(newRecord, new CurrencyView { });
            }
        }
    }
}
