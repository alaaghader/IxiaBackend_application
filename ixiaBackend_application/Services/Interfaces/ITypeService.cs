using ixiaBackend_application.Models.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ITypeService
    {
        Task<Result<List<TypeView>>> GetTypeAsync(int subCategoryId);
        Task<Result<List<TypeView>>> GetAllTypesAsync();
    }
}
