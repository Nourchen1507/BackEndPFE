using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryByNameAsync(string categoryName);
        Task<IEnumerable<Product>> GetAllProductsInCategoryAsync(Guid id);
    }
}
