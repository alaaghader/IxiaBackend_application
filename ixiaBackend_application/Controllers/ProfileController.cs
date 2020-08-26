using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
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
