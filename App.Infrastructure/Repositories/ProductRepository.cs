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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Product> _products;
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _products = _applicationDbContext.Set<Product>();
        }



        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await _applicationDbContext.Product
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            return await _applicationDbContext.Product
                .Where(p => p.CategoryName == categoryName)
                .ToListAsync();
        }
    }
}

