using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Services;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("GetAllFavorites/{id}")]
        public async Task<IActionResult> GetAllFavoritesAsync(string id)
        {
            var result = await _favoriteService.GetAllFavoritesAsync(id);
            return result.ToActionResult();
        }

        [HttpPost("ToggleFavorite")]
        public async Task<IActionResult> ToggleFavoritesAsync(string id,int prodId)
        {
            var result = await _favoriteService.ToggleProductFavoriteAsync(id, prodId);
            return result.ToActionResult();
        }
    }
}
