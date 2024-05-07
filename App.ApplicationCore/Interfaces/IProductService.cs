using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Domain.Dtos.Product;
using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public  interface IProductService
    {

        Task<IEnumerable<ReadProductDto>> GetAllProductsAsync(QueryOptions queryOptions);
        Task<ReadProductDto> CreateProductAsync(CreateProductDto createProductDto);
         Task<ReadProductDto> UpdateProductAsync(Guid productId, UpdateProductDto updateProductDto);
        Task<ReadProductDto> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<bool> DeleteProductByIdAsync(Guid productId);

        Task<IEnumerable<Product>> GetProductsByCategoryNameAsync(string Name);
    }
}
