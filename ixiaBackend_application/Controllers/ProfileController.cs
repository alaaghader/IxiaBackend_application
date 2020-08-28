using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ixiaBackend_application.Models.ModelsView;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<User> userManager;

        public ProfileController(IProfileService profileService,
            UserManager<User> userManager)
        {
            _profileService = profileService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Get signed-in user profile info
        /// </summary>
        /// <returns>User info</returns>
        [HttpGet]
        [Authorize]
        [Produces(typeof(Result<UserView>))]
        public async Task<IActionResult> GetUserAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _profileService.GetUserAsync(user.Id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get user profile info by id
        /// </summary>
        /// <returns>User info</returns>
        [HttpGet("{id}")]
        [Produces(typeof(Result<UserView>))]
        [Authorize, AllowAnonymous]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _profileService.GetUserAsync(id);
            return result.ToActionResult();
        }

        [HttpPost("EditProfile")]
        public async Task<IActionResult> UpdateProfileAsync(string id, ProfileInput profileInput)
        {
            var result = await _profileService.UpdateProfileAsync(id, profileInput);
            return result.ToActionResult();
        }
    }
}
