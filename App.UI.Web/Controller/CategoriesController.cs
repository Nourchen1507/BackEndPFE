using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Category;
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
    public class CategoriesController : ControllerBase
    {


        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoriesController(ICategoryService categoryService, ApplicationDbContext applicationDbContext)
        {
            _categoryService = categoryService;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<ReadCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _applicationDbContext.Category.ToListAsync();
            return Ok(categories);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReadCategoryDto>> CreateCategoryAsync([FromBody] CreateCategoryDto categoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(categoryDto);
            return Ok(category);
        }



        [HttpGet("{categoryId:Guid}")]
     
        public async Task<ActionResult<ReadCategoryDto>> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPut("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReadCategoryDto>> UpdateCategoryAsync(Guid id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var category = await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return Ok(category);
        }

        [HttpDelete("{id:Guid}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                NotFound();
                return false;
            }
            return true;
        }

        [HttpGet("{id:Guid}/products")]
        public async Task<ActionResult<ReadProductDto>> GetAllProductInCategoryAsync(Guid id)
        {
            var products = await _categoryService.GetAllProductsInCategoryAsync(id);
            return Ok(products);
        }
    }








}

