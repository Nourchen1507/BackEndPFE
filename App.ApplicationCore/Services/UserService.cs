using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator = App.ApplicationCore.Common.Validator;

namespace App.ApplicationCore.Services
{
    public class UserService : IUserService
    {



        private readonly IUserRepository _userRepository;
        private readonly ISanitizerService _sanitizerService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ISanitizerService sanitizerService, IMapper mapper)
        {
            _userRepository = userRepository;
            _sanitizerService = sanitizerService;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> CreateUserAsync(CreateUserDto userDto)
        {
            try
            {
                var sanitizedDto = _sanitizerService.SanitizeDto(userDto);

                var existingUser = await _userRepository.GetUserByEmailAsync(sanitizedDto.Email);

                var userDtoProperties = typeof(CreateUserDto).GetProperties();
                foreach (var property in userDtoProperties)
                {
                    var dtoValue = property.GetValue(userDto);
                    if (dtoValue is null or (object)"")
                    {
                        throw new ArgumentException($"{property.Name} is required.");
                    }
                }

                bool IsValidEmail = Validator.IsValidEmail(sanitizedDto.Email);

                if (!IsValidEmail)
                {
                    throw new ArgumentException("Invalid Email address.");
                }

                if (existingUser is not null)
                {
                    throw new ArgumentException("A user with this email already exist.");
                }

                var userEntity = _mapper.Map<User>(sanitizedDto);

                var hashedPassword = PasswordService.HashPassword(sanitizedDto.Password);
                userEntity.PasswordHash = hashedPassword;
                userEntity = await _userRepository.AddAsync(userEntity);

                var readUserDto = _mapper.Map<ReadUserDto>(userEntity);
                return readUserDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mapping error: " + ex.Message);

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                throw;
            }
        }

        public async Task<ReadUserDto> CreateAdminAsync(CreateUserDto userDto)
        {
            try
            {
                var sanitizedDto = _sanitizerService.SanitizeDto(userDto);

                var existingUser = await _userRepository.GetUserByEmailAsync(sanitizedDto.Email);

                var userDtoProperties = typeof(CreateUserDto).GetProperties();
                foreach (var property in userDtoProperties)
                {
                    var dtoValue = property.GetValue(userDto);
                    if (dtoValue is null or (object)"")
                    {
                        throw new ArgumentException($"{property.Name} is required.");
                    }
                }

                bool IsValidEmail = Common.Validator.IsValidEmail(sanitizedDto.Email);

                if (!IsValidEmail)
                {
                    throw new ArgumentException("Invalid Email address.");
                }

                if (existingUser is not null)
                {
                    throw new ArgumentException("A user with this email already exist.");
                }

                var userEntity = _mapper.Map<User>(sanitizedDto);

                var hashedPassword = PasswordService.HashPassword(sanitizedDto.Password);
                userEntity.PasswordHash = hashedPassword;

                userEntity = await _userRepository.CreateAdminAsync(userEntity);

                var readUserDto = _mapper.Map<ReadUserDto>(userEntity);
                return readUserDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mapping error: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                throw;
            }
        }

        public async Task<bool> DeleteUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            return await _userRepository.DeleteByIdAsync(userId);
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllUsersAsync(QueryOptions queryOptions)
        {
            var users = await _userRepository.GetAllAsync(queryOptions);
            var readUserDtos = _mapper.Map<IEnumerable<ReadUserDto>>(users);
            return readUserDtos;
        }

        public async Task<ReadUserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var readUserDto = _mapper.Map<ReadUserDto>(user);
            return readUserDto;
        }

        public async Task<ReadUserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var readUserDto = _mapper.Map<ReadUserDto>(user);
            return readUserDto;
        }

    }

}
