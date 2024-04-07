using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Category> _categories;

        public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _categories = _applicationDbContext.Set<Category>();
        }

        public async Task<IEnumerable<Product>> GetAllProductsInCategoryAsync(Guid id)
        {
            var category = await _categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            return category.Products;
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            return await _categories
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName.ToLower());
        }
    }
}
