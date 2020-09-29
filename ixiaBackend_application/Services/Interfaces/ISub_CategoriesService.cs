using ixiaBackend_application.Models.ModelsView;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ISub_CategoriesService
    {
        Task<Result<List<Sub_CategoryView>>> GetSubCategoryAsync(int categoryId);
    }
}
