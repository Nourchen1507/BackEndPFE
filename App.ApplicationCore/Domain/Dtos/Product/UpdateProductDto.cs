using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Product
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public int Inventory { get; set; }
        public string ImageUrl { get; set; }
    }
}
