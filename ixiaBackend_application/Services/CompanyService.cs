using AutoMapper;
using AutoMapper.QueryableExtensions;
using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IxiaContext _context;
        private readonly IMapper _mapper;

        public CompanyService(IxiaContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<bool>> AddCompanyAsync(CompanyInput companyInput)
        {
            Company company = new Company
            {
                Email = companyInput.Email,
                Name = companyInput.Name,
                PhoneNumber = companyInput.PhoneNumber
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Result<List<CompanyView>>> GetCompaniesAsync()
        {
            var result = await (from companies in _context.Companies
                               select companies)
                                     .Include(x => x.Products)
                                     .ProjectTo<CompanyView>(_mapper.ConfigurationProvider)
                                     .ToListAsync();
            return result;
        }

        public async Task<Result<CompanyView>> GetCompanyDetails(int id)
        {
            var result = await (from companies in _context.Companies
                               where companies.Id == id
                                select companies)
                                     .Include(x => x.Products)
                                     .ProjectTo<CompanyView>(_mapper.ConfigurationProvider)
                                     .FirstAsync();
            return result;
        }
    }
}
