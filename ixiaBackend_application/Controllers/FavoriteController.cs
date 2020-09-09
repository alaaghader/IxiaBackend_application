using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Services;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<User> userManager;

        public FavoriteController(IFavoriteService favoriteService,
            UserManager<User> userManager)
        {
            _favoriteService = favoriteService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get All Favorites
        /// </summary>
        /// <returns>All Favorites Details</returns>
        [HttpGet("GetAllFavorites")]
        [Authorize]
        public async Task<IActionResult> GetAllFavoritesAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _favoriteService.GetAllFavoritesAsync(user.Id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Toggle Favorite
        /// </summary>
        /// <param name="id">Product id</param>
        [HttpPost("ToggleFavorite/{id}")]
        [Authorize]
        public async Task<IActionResult> ToggleFavoritesAsync(int id)
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _favoriteService.ToggleProductFavoriteAsync(user.Id, id);
            return result.ToActionResult();
        }
    }
}
