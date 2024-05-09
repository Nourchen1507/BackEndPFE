using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
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


        public ProductController(IProductService productService, IMapper mapper, ApplicationDbContext applicationDbContext)
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

        [HttpPost("delete")]

        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductDto deleteProductDto)
        {
            try
            {
                // Vérifiez si l'ID du produit est valide (GUID)
                if (deleteProductDto == null || deleteProductDto.ProductId == Guid.Empty)
                {
                    return BadRequest("Invalid product ID.");
                }

                // Appelez le service pour supprimer le produit
                var result = await _productService.DeleteProductByIdAsync(deleteProductDto.ProductId);

                if (result)
                {
                    return Ok("Product deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete product.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while deleting the product.", message = ex.Message });
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDto updateProductDto, IFormFile image)
        {
            try
            {
                if (updateProductDto == null)
                {
                    return BadRequest("Invalid update data.");
                }

                // Vérifiez si le produit existe
                var existingProduct = await _productService.GetProductByIdAsync(updateProductDto.ProductId);
                if (existingProduct == null)
                {
                    return NotFound("Product not found.");
                }

                // Mettez à jour les propriétés du produit avec les données de DTO
                existingProduct.Name = updateProductDto.Name;
                existingProduct.Description = updateProductDto.Description;
                existingProduct.Price = updateProductDto.Price;
                existingProduct.CategoryId = updateProductDto.CategoryId; // Mettez à jour l'ID de la catégorie

                // Enregistrez le chemin d'accès à la nouvelle image si elle est fournie
                if (image != null && image.Length > 0)
                {
                    // Générez un nom de fichier unique pour éviter les collisions
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                    // Enregistrez l'image sur le serveur
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    // Mettez à jour le chemin d'accès à l'image dans le produit
                    existingProduct.ImageUrl = $"/images/{uniqueFileName}";
                }

                // Appelez le service pour mettre à jour le produit
                var result = await _productService.UpdateProductAsync(existingProduct.Id, updateProductDto);

                if (result != null)
                {
                    return Ok("Product updated successfully.");
                }
                else
                {
                    return BadRequest("Failed to update product.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while updating the product.", message = ex.Message });
            }
        }
    }
}