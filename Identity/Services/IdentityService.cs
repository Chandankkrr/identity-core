﻿using Identity.Interfaces;
using Identity.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(UserManager<IdentityUser> userManager, ITokenService tokenService, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNotFoundMessage = $"User with email: {email} does not exist";

                _logger.LogWarning(userNotFoundMessage);

                return new LoginResponse { Errors = new string[] { userNotFoundMessage } };
            }

            var isValidEmailPasswordCombination = await _userManager.CheckPasswordAsync(user, password);

            if (!isValidEmailPasswordCombination)
            {
                _logger.LogWarning($"Login failed for user, email or password is incorrect");

                return new LoginResponse { Errors = new string[] { "Email or password is incorrect" } };
            }

            var token = _tokenService.GenerateToken(user.Id);

            return new LoginResponse
            {
                Success = true,
                Token = token
            };
        }

        public async Task<RegistrationResponse> RegisterAsync(IdentityUser user, string password)
        {
            var email = user.Email;
            var userExists = await _userManager.FindByEmailAsync(email);

            if (userExists != null)
            {
                return new RegistrationResponse
                {
                    Errors = new[] { $"User with email {email} already exists" }
                };
            }

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new RegistrationResponse
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            var token = _tokenService.GenerateToken(user.Id);

            return new RegistrationResponse
            {
                Success = true,
                Token = token
            };
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return emailConfirmationToken;
        }

        public async Task<EmailConfirmationResponse> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                var userNotFoundMessage = $"User with email: {email} does not exist";

                _logger.LogWarning(userNotFoundMessage);

                return new EmailConfirmationResponse
                {
                    Errors = new[] { userNotFoundMessage }
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return new EmailConfirmationResponse
                {
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new EmailConfirmationResponse
            {
                EmailConfirmed = true,
                Success = true
            };
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }
    }
}
