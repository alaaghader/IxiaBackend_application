using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<bool>> AddCategoryAsync(CategoryInput categoryInput);

        Task<Result<List<CategoryView>>> GetAllCategoriesAsync();
    }
}
