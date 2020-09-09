using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<ProductView>>> GetAllProductsAsync(string userId);
        Task<Result<ProductView>> GetProductDetailsAsync(int id, string userId);
        Task<Result<bool>> AddProductAsync(ProductInput productInput);
        Task<Result<List<ProductView>>> SearchProductsAsync(string name, string userId);
    }
}
