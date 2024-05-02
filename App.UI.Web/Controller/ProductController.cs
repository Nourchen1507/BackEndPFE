using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
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


        public ProductController (IProductService productService, ApplicationDbContext applicationDbContext)
           {

            _productService = productService;
            _applicationDbContext = applicationDbContext;
        }




        [HttpGet]
        public async Task<ActionResult<ReadProductDto>> GetAllProductsAsync([FromQuery] QueryOptions queryOptions)
        {
            var products = await _applicationDbContext.Products.ToListAsync();
            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
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
