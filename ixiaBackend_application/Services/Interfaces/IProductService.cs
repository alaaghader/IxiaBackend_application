using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<ProductView>>> GetAllProductsAsync();
        Task<Result<ProductView>> GetProductDetailsAsync(int id);
        Task<Result<bool>> AddProductAsync(ProductInput productInput);
        Task<Result<List<ProductView>>> SearchProductsAsync(string name);
    }
}
