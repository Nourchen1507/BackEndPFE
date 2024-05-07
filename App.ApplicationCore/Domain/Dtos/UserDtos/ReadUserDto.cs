using App.ApplicationCore.Domain.Dtos.Adresses;
using App.ApplicationCore.Domain.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.UserDtos
{
    public class ReadUserDto
    {

        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Localisation { get; set; }
        public ReadAdresseDto Address { get; set; }
        public string CIN { get; set; }
    }
  
}
