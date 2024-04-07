using App.ApplicationCore.Domain.Dtos.Order;
using App.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.UI.Web.Controller
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _ordersService;

        public OrderController(IOrderService ordersService)
        {
            _ordersService = ordersService;
        }


        [HttpPost("Users/{userId:Guid}/Orders")]
        [Authorize(Policy = "ProfileOwnerOnly")]
        public async Task<ActionResult<ReadOrderDto>> CreateOrderAsync(Guid userId, [FromBody] CreateOrderDto orderDto)
        {
            var newOrder = await _ordersService.CreateOrderAsync(userId, orderDto);
            return Ok(newOrder);
        }

    }



}
