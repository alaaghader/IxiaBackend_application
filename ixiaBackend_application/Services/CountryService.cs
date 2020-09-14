using AutoMapper;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public CountryService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<CountryView>> AddCountryAsync(string name)
        {
            var inTable = await _context.Countries.SingleOrDefaultAsync(e => e.Name == name);
            if (inTable != null)
                return Result.Conflict<CountryView>().With(Error.CountryAlreadyExists());
            else 
            {
                var newRecord = new Country 
                { 
                    Name = name,
                };
                await _context.Countries.AddAsync(newRecord);
                await _context.SaveChangesAsync();

                return _mapper.Map(newRecord, new CountryView { });
            }
        }
    }
}
