﻿using ixiaBackend_application.Models;
using ixiaBackend_application.Models.Entities;
using ixiaBackend_application.Models.ModelsView;
using ixiaBackend_application.ModelsInput;
using ixiaBackend_application.Options;
using ixiaBackend_application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace ixiaBackend_application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IxiaContext _context;
        private readonly Security securityOptions;

        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IxiaContext ixiaContext,
            IOptions<Security> securityOptions)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = ixiaContext;
            this.securityOptions = securityOptions.Value;
        }

        public async Task<Result<TokenView>> CreateUserAsync(SignupInput signUpInput)
        {
            var user = await userManager.FindByEmailAsync(signUpInput.Email);

            if (user != null)
            {
                return Result.Conflict<TokenView>().With(Error.EmailAlreadyExists(user.Provider));
            }

            var newUser = new User
            {
                FirstName = signUpInput.FirstName,
                LastName = signUpInput.LastName,
                UserName = signUpInput.UserName,
                Email = signUpInput.Email,
                Provider = "Ixia"
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

            if(user.Provider != "Ixia")
            {
                return Result.Conflict<TokenView>().With(Error.EmailAlreadyExists(user.Provider));
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(user, signInInput.Password, false);
            if (!signInResult.Succeeded)
            {
                return Result.Unauthorized<TokenView>().With(Error.WrongUsernameOrPassword());
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);
            var creds = new SigningCredentials(securityOptions.SecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var expiresOn = DateTime.UtcNow.AddHours(securityOptions.ExpireInDays);
            var token = new JwtSecurityToken(issuer: securityOptions.Issuer, audience: securityOptions.Audiance,
                signingCredentials: creds, claims: principal.Claims, expires: expiresOn);
            await userManager.UpdateAsync(user);

            return new TokenView
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresOn = token.ValidTo,
                Email = signInInput.Email,
                UserId = user.Id
            };
        }

        public async Task<Result<TokenView>> SignInWithFacebook(FacebookSignInInput facebookSignInInput)
        {
            var user = await userManager.FindByEmailAsync(facebookSignInInput.Email);
            if(user != null)
            {
                if(user.Provider != "Facebook")
                {
                    return Result.Conflict<TokenView>().With(Error.EmailAlreadyExists(user.Provider));
                }
                else
                {
                    var principal = await signInManager.CreateUserPrincipalAsync(user);
                    var creds = new SigningCredentials(securityOptions.SecurityKey, SecurityAlgorithms.HmacSha256Signature);
                    var expiresOn = DateTime.UtcNow.AddHours(securityOptions.ExpireInDays);
                    var token = new JwtSecurityToken(issuer: securityOptions.Issuer, audience: securityOptions.Audiance,
                        signingCredentials: creds, claims: principal.Claims, expires: expiresOn);
                    await userManager.UpdateAsync(user);

                    return new TokenView
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpiresOn = token.ValidTo,
                        Email = facebookSignInInput.Email,
                        UserId = user.Id
                    };
                }
            }
            else
            {
                var newUser = new User {
                    Email = facebookSignInInput.Email,
                    UserName = facebookSignInInput.Email,
                    Provider = "Facebook",
                };

                await userManager.CreateAsync(newUser);

                var principal = await signInManager.CreateUserPrincipalAsync(newUser);
                var creds = new SigningCredentials(securityOptions.SecurityKey, SecurityAlgorithms.HmacSha256Signature);
                var expiresOn = DateTime.UtcNow.AddHours(securityOptions.ExpireInDays);
                var token = new JwtSecurityToken(issuer: securityOptions.Issuer, audience: securityOptions.Audiance,
                    signingCredentials: creds, claims: principal.Claims, expires: expiresOn);
                await userManager.UpdateAsync(newUser);

                return new TokenView
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresOn = token.ValidTo,
                    Email = facebookSignInInput.Email,
                    UserId = newUser.Id
                };
            }
        }

        public async Task<Result<TokenView>> SignInWithGoogle(GoogleSignInInput googleSignInInput)
        {
            var user = await userManager.FindByEmailAsync(googleSignInInput.Email);
            if (user != null)
            {
                if (user.Provider != "Google")
                {
                    return Result.Conflict<TokenView>().With(Error.EmailAlreadyExists(user.Provider));
                }
                else
                {
                    var principal = await signInManager.CreateUserPrincipalAsync(user);
                    var creds = new SigningCredentials(securityOptions.SecurityKey, SecurityAlgorithms.HmacSha256Signature);
                    var expiresOn = DateTime.UtcNow.AddHours(securityOptions.ExpireInDays);
                    var token = new JwtSecurityToken(issuer: securityOptions.Issuer, audience: securityOptions.Audiance,
                        signingCredentials: creds, claims: principal.Claims, expires: expiresOn);
                    await userManager.UpdateAsync(user);

                    return new TokenView
                    {
                        AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                        ExpiresOn = token.ValidTo,
                        Email = googleSignInInput.Email,
                        UserId = user.Id
                    };
                }
            }
            else
            {
                var newUser = new User
                {
                    Email = googleSignInInput.Email,
                    UserName = googleSignInInput.Email,
                    Provider = "Google",
                };

                await userManager.CreateAsync(newUser);

                var principal = await signInManager.CreateUserPrincipalAsync(newUser);
                var creds = new SigningCredentials(securityOptions.SecurityKey, SecurityAlgorithms.HmacSha256Signature);
                var expiresOn = DateTime.UtcNow.AddHours(securityOptions.ExpireInDays);
                var token = new JwtSecurityToken(issuer: securityOptions.Issuer, audience: securityOptions.Audiance,
                    signingCredentials: creds, claims: principal.Claims, expires: expiresOn);
                await userManager.UpdateAsync(newUser);

                return new TokenView
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresOn = token.ValidTo,
                    Email = googleSignInInput.Email,
                    UserId = newUser.Id
                };
            }
        }
    }
}
