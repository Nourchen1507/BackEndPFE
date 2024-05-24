using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Domain.Dtos.UserDtos
{
      public  class UpdateUserDto
    {


        
              public Guid UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public Guid AdresseId { get; set; }
            public string Localisation { get; set; }
            public string Phone { get; set; }

             public string Password { get; set; }
 






    }
}
