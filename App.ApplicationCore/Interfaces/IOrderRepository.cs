using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId);
        Task<Order> GetByIdWithOtherDetailsAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}
