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
    public  class OrderRepository : GenericRepository<Order>, IOrderRepository
    {


        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<Order> _orders;

        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _orders = _applicationDbContext.Set<Order>();
        }

        public async Task<Order> GetByIdWithOtherDetailsAsync(Guid orderId)
        {
            return await _orders
                            .Include(order => order.OrderItems)
                            .ThenInclude(orderItem => orderItem.Product)
                            .FirstOrDefaultAsync(order => order.Id == orderId);
        }
        public async Task<IEnumerable<Order>> GetOrdersForUserAsync(Guid userId)
        {
            return await _orders
                            .Where(o => o.UserId == userId)
                            .Include(order => order.OrderItems)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync()
        {
            return await _orders
                            .Include(order => order.OrderItems)
                            .ThenInclude(orderItem => orderItem.Product)
                            .ToListAsync();
        }
    }
}
