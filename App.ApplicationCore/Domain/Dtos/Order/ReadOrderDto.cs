using App.ApplicationCore.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Order
{
    public class ReadOrderDto
    {
        public Guid Id { get; set; }
        public ReadUserDto User { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ReadOrderItemDto> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
