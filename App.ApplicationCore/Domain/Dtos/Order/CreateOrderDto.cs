using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Order
{
    public class CreateOrderDto
    {
        public required List<OrderItemDto> Items { get; set; }

        public Guid AdresseId { get; set; }

        public Guid UserId { get; set; }
    }
}
