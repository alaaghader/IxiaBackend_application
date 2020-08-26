using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration _config;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _config = config;
        }

        public async Task<Result<TokenView>> CreateUserAsync(SignupInput signUpInput)
        {
            var user = await userManager.FindByEmailAsync(signUpInput.Email);

            if (user != null)
            {
                return Result.Conflict<TokenView>().With(Error.EmailAlreadyExists());
            }

            var newUser = new User
            {
                FirstName = signUpInput.FirstName,
                LastName = signUpInput.LastName,
                UserName = signUpInput.UserName,
                Email = signUpInput.Email
            };

            SigninInput signInInput = new SigninInput
            {
                Email = signUpInput.Email,
                Password = signUpInput.Password
            };

            var result = await userManager.CreateAsync(newUser, signUpInput.Password);

            if (!result.Succeeded)
            {
                var errors = new List<Error>();
                foreach (var error in result.Errors)
                {
                    errors.Add(new Error(error.Code, error.Description));
                }
                return Result.Conflict<TokenView>().With(errors.ToArray());
            }

            return await SignInAsync(signInInput);
        }

        public async Task<Result<TokenView>> SignInAsync(SigninInput signInInput)
        {
            var user = await userManager.FindByEmailAsync(signInInput.Email);

            if (user == null)
            {
                return Result.Unauthorized<TokenView>().With(Error.WrongUsernameOrPassword());
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, signInInput.Password, false);
            if (!signInResult.Succeeded)
            {
                return Result.Unauthorized<TokenView>().With(Error.WrongUsernameOrPassword());
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("DateOfJoing", user.BirthDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new TokenView
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Email = user.Email,
                UserId = user.Id,
                UserName = user.UserName
            };
        }
    }
}
