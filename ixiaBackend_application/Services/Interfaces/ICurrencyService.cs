using ixiaBackend_application.Models.ModelsView;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<CurrencyView>> AddCurrencyAsync(string currencyName);
    }
}
