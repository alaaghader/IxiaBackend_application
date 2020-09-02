using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IProfileService
    {
        Task<Result<UserView>> GetUserAsync(string userId);
        Task<Result<UserView>> UpdateProfileAsync(string userId, ProfileInput profileInput);
    }
}
