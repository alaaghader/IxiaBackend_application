using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ixiaBackend_application.Helpers;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ixiaBackend_application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private IAccountService _account;
        private SignInManager<User> signInManager;
        private IConfiguration _config;

        public AccountController(
            IAccountService account,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _account = account;
            this.signInManager = signInManager;
           _config = config;
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        /// <param name="input">User sign up information</param>
        /// <returns>A valid access token</returns>
        [HttpPost("SignUp")]
        [Produces(typeof(Result<TokenView>))]
        public async Task<IActionResult> SignUpAsync(SignupInput input)
        {
            var result = await _account.CreateUserAsync(input);
            return result.ToActionResult();
        }

        /// <summary>
        /// Log a user in
        /// </summary>
        /// <param name="input">User email and password</param>
        /// <returns>A valid access token</returns>
        [HttpPost("Login")]
        [Produces(typeof(Result<TokenView>))]
        public async Task<IActionResult> LoginAsync(SigninInput input)
        {
            var result = await _account.SignInAsync(input);
            return result.ToActionResult();
        }

        /// <summary>
        /// Facebook login
        /// </summary>
        /// <param name="input">Facebook email</param>
        /// <returns>A valid access token</returns>
        [HttpPost("FacebookLogin")]
        [Produces(typeof(Result<TokenView>))]
        public async Task<IActionResult> FacebookLoginAsync(FacebookSignInInput input)
        {
            var result = await _account.SignInWithFacebook(input);
            return result.ToActionResult();
        }

        /// <summary>
        /// Google login
        /// </summary>
        /// <param name="input">Google email</param>
        /// <returns>A valid access token</returns>
        [HttpPost("GoogleLogin")]
        [Produces(typeof(Result<TokenView>))]
        public async Task<IActionResult> GoogleLoginAsync(GoogleSignInInput input)
        {
            var result = await _account.SignInWithGoogle(input);
            return result.ToActionResult();
        }

        [HttpGet("hashem/{token}")]
        [AllowAnonymous]
        public async Task<HttpStatusCode> ValidateTokenAsync(string token) {
            var userInfoUrl = "http://localhost:5000/api/Profile"; 
            var hc = new HttpClient();
            hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = hc.GetAsync(userInfoUrl).Result;
            dynamic userInfo = response.Content.ReadAsStringAsync().Result;
            return response.StatusCode;
        }

        //  [HttpGet("hi")]
        //  public async Task<IActionResult> Test()
        //  {
        //      return Ok(new {message = "hi" });
        //  }

        //  [HttpGet("ExternalLogin")]
        //  public async Task<IActionResult> ExternalLogin()
        //  {
        //      IList<AuthenticationScheme> ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //
        //      foreach (var provider in ExternalLogins)
        //      {
        //          if (provider.Name == "Google")
        //          {
        //              var redirectUrl = Url.Action("ExternalLoginCallBack", "Account",
        //                      new { ReturnUrl = "" });
        //              var properties = signInManager.ConfigureExternalAuthenticationProperties(provider.Name, redirectUrl);
        //              return new ChallengeResult(provider.Name, properties);
        //          }
        //      }
        //      return BadRequest();
        //  }

        //  [AllowAnonymous]
        //  public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null)
        //  {
        //      returnUrl = returnUrl ?? Url.Content("~/");
        //
        //      if (remoteError != null)
        //      {
        //          return BadRequest();
        //      }
        //
        //      var info = await signInManager.GetExternalLoginInfoAsync();
        //      if (info == null)
        //      {
        //          return BadRequest();
        //      }
        //
        //      var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
        //                          info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //
        //      if (signInResult.Succeeded)
        //      {
        //          var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //          var user = await _userManager.FindByEmailAsync(email);
        //
        //          var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //          var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //
        //          var claims = new[] {
        //              new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //              new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //              new Claim("DateOfJoing", user.BirthDate.ToString("yyyy-MM-dd")),
        //              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //          };
        //
        //          var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //              _config["Jwt:Issuer"],
        //              claims,
        //              expires: DateTime.Now.AddMinutes(120),
        //              signingCredentials: credentials);
        //
        //
        //          return Ok(new TokenView{
        //              AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
        //              ExpiresOn = token.ValidTo,
        //              Email = user.Email,
        //              UserId = user.Id,
        //              UserName = user.UserName
        //          });
        //      }
        //      else
        //      {
        //          var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //
        //          if (email != null)
        //          {
        //              var user = await _userManager.FindByEmailAsync(email);
        //
        //              if (user == null)
        //              {
        //                  user = new User
        //                  {
        //                      UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //                      Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        //                  };
        //
        //                  await _userManager.CreateAsync(user);
        //              }
        //
        //              var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //              var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //
        //              var claims = new[] {
        //                  new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //                  new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //                  new Claim("DateOfJoing", user.BirthDate.ToString("yyyy-MM-dd")),
        //                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //              };
        //
        //              var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //                  _config["Jwt:Issuer"],
        //                  claims,
        //                  expires: DateTime.Now.AddMinutes(120),
        //                  signingCredentials: credentials);
        //              await _userManager.AddLoginAsync(user, info);
        //              await signInManager.SignInAsync(user, isPersistent: false);
        //
        //              return Ok(new TokenView
        //              {
        //                  AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
        //                  ExpiresOn = token.ValidTo,
        //                  Email = user.Email,
        //                  UserId = user.Id,
        //                  UserName = user.UserName
        //              });
        //          }
        //          return BadRequest();
        //      }
        //  }
    }
}
