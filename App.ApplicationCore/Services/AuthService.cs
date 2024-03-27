﻿using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
  public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;

        private readonly IJwtManager _jwtManager;
            public AuthService(IUserRepository userRepository, IJwtManager jwtManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
        }

        public async Task<string> AutheticateUser(UserCredentialsDto userCredentials)
        {
            var user = await _userRepository.GetUserByEmailAsync(userCredentials.Email)
                 ?? throw new ArgumentException("Invalid login credentials.");
            var isAuthenticated = PasswordService.VerifyPassword(user.PasswordHash, userCredentials.Password);
            if (!isAuthenticated)
            {
                throw new ArgumentException("Invalid login credentials");
            }
            string token = _jwtManager.GenerateAccessToken(user);
            return token;
        }

        public async Task<string> RefreshToken(string refreshToken)
        {
            return await _jwtManager.RefreshAccessToken(refreshToken);
        }
    }
}
