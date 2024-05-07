using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class AdresseService : IAdresseService
    {

        private readonly IAdresseRepository _adresseRepository;
        private readonly ISanitizerService _sanitizerService;
        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;




        public AdresseService(IAdresseRepository AdresseRepository, IHttpContextAccessor httpContextAccessor,
            ISanitizerService sanitizerService, IMapper mapper)
        {
            _adresseRepository = AdresseRepository;
            _sanitizerService = sanitizerService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<ReadAdresseDto> CreateAddresseAsync(CreateAdresseDto adresseDto)
        {
            try
            {
                var sanitizedDto = _sanitizerService.SanitizeDto(adresseDto);

                var existingAdresse = await _adresseRepository.GetAdresseByResidenceNameAsync(sanitizedDto.ResidenceName);
                if (existingAdresse != null)
                {
                    throw new ArgumentException($"Une adresse portant le nom `{sanitizedDto.ResidenceName}` existe déjà.");
                }

                var adresseDtoProperties = typeof(CreateAdresseDto).GetProperties();
                foreach (var property in adresseDtoProperties)
                {
                    var dtoValue = property.GetValue(sanitizedDto);
                    if (dtoValue is null or (object)"")
                    {
                        throw new ArgumentException($"{property.Name} est requis.");
                    }
                }

                var newAdresse = _mapper.Map<Adresse>(sanitizedDto);
                newAdresse = await _adresseRepository.AddAsync(newAdresse);

                var readAdresseDto = _mapper.Map<ReadAdresseDto>(newAdresse);
                return readAdresseDto;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Exception interne : " + ex.InnerException.Message);
                }
                throw;
            }
        }


        public async Task<IEnumerable<ReadAdresseDto>> GetAllAdressesAsync(QueryOptions queryOptions)
        {
            var adresse = await _adresseRepository.GetAllAsync();
            var readaddresseDto = _mapper.Map<IEnumerable<ReadAdresseDto>>(adresse);
            return readaddresseDto;
        }
        public async Task<ReadAdresseDto> GetAdresseByIdAsync(Guid adresseId)
        {
            var adresse = await _adresseRepository.GetByIdAsync(adresseId)
                ?? throw new ArgumentException($"Category with ID {adresseId} not found.");
            var readaddresseDto = _mapper.Map<ReadAdresseDto>(adresse);
            return readaddresseDto;
        }

        public Task<bool> DeleteAddresseByIdAsync(Guid adresseId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReadAdresseDto>> GetAllAddresseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ReadAdresseDto> UpdateAddresseAsync(Guid adresseId, UpdateAdresseDto addresseDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllUsersInAddresseAsync(Guid adresseId)
        {
            var users = await _adresseRepository.GetAllUsersInAdresseAsync(adresseId);
            var readUserDtos = _mapper.Map<IEnumerable<ReadUserDto>>(users);
            return readUserDtos;
        }
       

    }
}
