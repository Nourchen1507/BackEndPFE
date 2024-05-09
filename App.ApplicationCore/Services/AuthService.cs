using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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

        //public async Task<ResponseLoginDto> AutheticateUser(UserCredentialsDto userCredentials)
        //{
        //    var user = await _userRepository.GetUserByEmailAsync(userCredentials.Email)
        //        ?? throw new ArgumentException("Invalid login credentials.");
        //    var isAuthenticated = PasswordService.VerifyPassword(user.PasswordHash, userCredentials.Password);
        //    if (!isAuthenticated)
        //    {
        //        throw new ArgumentException("Invalid login credentials");
        //    }
        //    string token = _jwtManager.GenerateAccessToken(user);


        //    ResponseLoginDto responseLoginDto = new ResponseLoginDto();
        //    responseLoginDto.token = token;
        //    return responseLoginDto;
        //}

        //public async Task<ResponseLoginDto> AuthenticateUser(UserCredentialsDto userCredentials)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetUserByEmailAsync(userCredentials.Email);

        //        if (user == null)
        //        {
        //            throw new ArgumentException("Invalid login credentials.");
        //        }

        //        var isAuthenticated = PasswordService.VerifyPassword(user.PasswordHash, userCredentials.Password);

        //        if (!isAuthenticated)
        //        {
        //            throw new ArgumentException("Invalid login credentials.");
        //        }

        //        string token = _jwtManager.GenerateAccessToken(user);

        //        var responseLoginDto = new ResponseLoginDto
        //        {
        //            token = token,
        //            email = user.Email, // Ajout de l'email de l'utilisateur
        //            userId = user.Id, // Ajout de l'ID de l'utilisateur
        //            Role = user.Role // Ajout de la propriété role
        //        };

        //        return responseLoginDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine(ex.Message);
        //        throw new ArgumentException("Authentication failed.");
        //    }
        //}

        //public async Task<UserDto> GetUserById(int userId)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetUserByIdAsync(userId);

        //        if (user == null)
        //        {
        //            throw new ArgumentException("User not found.");
        //        }

        //        var userDto = new UserDto
        //        {
        //            Id = user.Id,
        //            Name = user.Name,
        //            // Add other properties as needed
        //        };

        //        return userDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Error.WriteLine(ex.Message);
        //        throw new ArgumentException("Failed to fetch user.");
        //    }
        //}
        public async Task<ResponseLoginDto> AuthenticateUser(UserCredentialsDto userCredentials)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(userCredentials.Email);

                if (user == null)
                {
                    throw new AuthenticationException("Invalid login credentials.");
                }

                var isAuthenticated = PasswordService.VerifyPassword(user.PasswordHash, userCredentials.Password);

                if (!isAuthenticated)
                {
                    throw new AuthenticationException("Invalid login credentials.");
                }

                string token = _jwtManager.GenerateAccessToken(user);

                var responseLoginDto = new ResponseLoginDto
                {
                    token = token,
                    email = user.Email,
                    userId = user.Id,
                    Role = user.Role
                };

                return responseLoginDto;
            }
            catch (AuthenticationException ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw; // Ré-émet l'exception sans la modifier
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                throw new AuthenticationException("Authentication failed.");
            }
        }


        public async Task<string> RefreshToken(string refreshToken)
        {
            return await _jwtManager.RefreshAccessToken(refreshToken);
        }
    }
}
