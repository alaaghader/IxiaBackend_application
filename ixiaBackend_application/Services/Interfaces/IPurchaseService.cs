using ixiaBackend_application.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<Result<List<PurchaseView>>> GetAllPurchasesAsync(string userId);
        Task<Result<bool>> TooglePurchaseAsync(string userId, int productId, string comments);
    }
}
