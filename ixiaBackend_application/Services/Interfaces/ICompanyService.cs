using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<Result<List<CompanyView>>> GetCompaniesAsync();
        Task<Result<CompanyView>> GetCompanyDetails(int id);
        Task<Result<bool>> AddCompanyAsync(CompanyInput companyInput);
    }
}
