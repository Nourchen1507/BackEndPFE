using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<ReadOrderDto>> GetAllOrdersAsync(QueryOptions queryOptions);
        Task<IEnumerable<ReadOrderDto>> GetOrdersWithDetailsdAsync();
        Task<ReadOrderDto> CreateOrderAsync(Guid userId, CreateOrderDto createOrderDto);
        //Task<ReadOrderDto> UpdateOrderAsync(Guid OrderId, UpdateOrderDto updateOrderDto);
        Task<ReadOrderDto> GetOrderByIdAsync(Guid orderId);
        Task<bool> DeleteOrderByIdAsync(Guid orderId);
        Task<IEnumerable<ReadOrderDto>> GetOrdersByUserIdAsync(Guid userId);
    }
}
