﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Order
{
    public class OrderItemDto
    {

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
