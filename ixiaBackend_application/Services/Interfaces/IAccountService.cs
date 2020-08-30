using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Result<TokenView>> CreateUserAsync(SignupInput signUpInput);
        Task<Result<TokenView>> SignInAsync(SigninInput signInInput);
        Task<Result<TokenView>> SignInWithFacebook(FacebookSignInInput facebookSignInInput);
    }
}
