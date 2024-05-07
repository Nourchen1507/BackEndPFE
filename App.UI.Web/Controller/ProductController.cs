using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Domain.Dtos.Product;
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
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;


        public ProductController (IProductService productService, IMapper mapper, ApplicationDbContext applicationDbContext)
           {

            _productService = productService;
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }




        [HttpGet]
        public async Task<ActionResult<ReadProductDto>> GetAllProductsAsync([FromQuery] QueryOptions queryOptions)
        {
            var products = await _applicationDbContext.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{productId:Guid}")]
        public async Task<ActionResult<ReadProductDto>> GetProductByIdAsync(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }



        [HttpGet("/category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryIdAsync(categoryId);
                var readProductDtos = _mapper.Map<IEnumerable<ReadProductDto>>(products);
                return Ok(readProductDtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("/categoryByName/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetProductsByCategoryNameAsync(string categoryName)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryNameAsync(categoryName);
                var readProductDtos = _mapper.Map<IEnumerable<ReadProductDto>>(products);
                return Ok(readProductDtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReadProductDto>> CreatProductAsync([FromBody] CreateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newProduct = await _productService.CreateProductAsync(product);
            return Ok(newProduct);
        }


    }
}
