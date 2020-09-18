using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
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
        public async Task<IActionResult> GetUserAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _profileService.GetUserAsync(user.Id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get user profile info by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User info</returns>
        [HttpGet("{id}")]
        [Authorize, AllowAnonymous]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            var result = await _profileService.GetUserAsync(id);
            return result.ToActionResult();
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="profileInput">User profile input</param>
        /// <returns>Update Profile</returns>
        [HttpPost("EditProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAsync([FromForm] ProfileInput profileInput)
        {
            var user = await userManager.GetUserAsync(User);
            var result = await _profileService.UpdateProfileAsync(user.Id, profileInput);
            return result.ToActionResult();
        }
    }
}
