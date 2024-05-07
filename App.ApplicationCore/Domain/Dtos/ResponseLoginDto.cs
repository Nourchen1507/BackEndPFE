using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos
{
   public class ResponseLoginDto
    {


    
        public string email { get; set; }
        public  string token { get; set; }
  
        public Guid userId { get; set; }
        public UserRole Role { get; set; }
    }
}
