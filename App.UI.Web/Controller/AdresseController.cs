using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using App.ApplicationCore.Interfaces;
using App.ApplicationCore.Services;
using App.Infrastructure.Persistance;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.UI.Web.Controller
{


    [ApiController]
    [Route("api/[controller]")]
    public class AdresseController : ControllerBase
    {


        private readonly IAdresseService _adresseService;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _applicationDbContext;
        public  AdresseController(IAdresseService adresseService, IMapper mapper, ApplicationDbContext applicationDbContext)
        {
            _adresseService = adresseService;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }




        [HttpGet]
        public async Task<ActionResult<ReadAdresseDto>> GetAllAdrressesAsync()
        {
            var adresse = await _applicationDbContext.Adresse.ToListAsync();
            return Ok(adresse);
        }


        [HttpPost]
       
        public async Task<ActionResult<ReadAdresseDto>> CreateCategoryAsync([FromBody] CreateAdresseDto addresseDto)
        {
            var adresse = await _adresseService.CreateAddresseAsync(addresseDto);
            return Ok(adresse);
        }

        [HttpGet("{id:Guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadCategoryDto>> GetAdresseByIdAsync(Guid id)
        {
            var adresse = await _adresseService.GetAdresseByIdAsync(id);
            if ( adresse == null)
            {
                return NotFound();
            }
            return Ok(adresse);
        }


        [HttpGet("{id:Guid}/users")]
        public async Task<ActionResult<ReadProductDto>> GetAllUsersInAddresseAsync(Guid id)
        {
            var products = await
            _adresseService.GetAllUsersInAddresseAsync(id);
            return Ok(products);
        }
    }



}

