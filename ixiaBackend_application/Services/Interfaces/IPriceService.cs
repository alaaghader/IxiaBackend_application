using ixiaBackend_application.Models.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IPriceService
    {
        Task<Result<PriceView>> AddPriceAsync(int prodiId, int countryId, int currencyId, double price);
        Task<Result<List<PriceView>>> GetPricesByCountry(string countryName);
        Task<Result<List<PriceView>>> SearchPricesByCountry(string countryName, string prodName);
    }
}
