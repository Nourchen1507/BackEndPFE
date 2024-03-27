using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Entities
{
   public class OrderItem: TimeStamp

    {
       
        public Guid OrderId { get; set; }
       
        public virtual Order Order { get; set; }
    
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
