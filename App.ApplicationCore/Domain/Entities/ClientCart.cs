using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
    public class ClientCart
    {



        public ClientCart()
        {
        }

        public ClientCart(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}

