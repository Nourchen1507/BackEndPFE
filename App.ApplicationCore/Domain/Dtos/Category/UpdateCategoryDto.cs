using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.Category
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
    }
}
