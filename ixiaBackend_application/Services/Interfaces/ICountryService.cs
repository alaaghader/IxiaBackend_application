using ixiaBackend_application.Models.ModelsView;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ICountryService
    {
        Task<Result<CountryView>> AddCountryAsync(string name);
    }
}
