using ixiaBackend_application.Models.ModelsView;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IFavoriteService
    {
        Task<Result<List<FavoriteView>>> GetAllFavoritesAsync(string userId);
        Task<Result<bool>> ToggleProductFavoriteAsync(string userId, int productId, int countryId, int currencyId);
    }
}
