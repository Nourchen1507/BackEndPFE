using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Web.Controller
{



    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _ordersService;

        public OrderController(IOrderService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReadOrderDto>> GetAllOrdersAsync()
        {
            var orders = await _ordersService.GetOrdersWithDetailsdAsync();
            return Ok(orders);
        }

        [HttpPost("Users/{userId:Guid}/Orders")]
        [Authorize(Policy = "ProfileOwnerOnly")]
        public async Task<ActionResult<ReadOrderDto>> CreateOrderAsync(Guid userId, [FromBody] CreateOrderDto orderDto)
        {
            var newOrder = await _ordersService.CreateOrderAsync(userId, orderDto);
            return Ok(newOrder);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = "AdminOrProfileOwner")]
        public async Task<ActionResult<ReadOrderDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "ProfileOwnerOnly")]
        public async Task<ActionResult<bool>> DeleteOrderAsync(Guid id)
        {
            return Ok(await _ordersService.DeleteOrderByIdAsync(id));
        }

        [HttpGet("User/{userId:Guid}")]
        [Authorize(Policy = "ProfileOwnerOnly")]
        public async Task<ActionResult<IEnumerable<ReadOrderDto>>> GetOrdersByUserIdAsync(Guid userId)
        {
            var userOrders = await _ordersService.GetOrdersByUserIdAsync(userId);
            return Ok(userOrders);
        }
    }
}