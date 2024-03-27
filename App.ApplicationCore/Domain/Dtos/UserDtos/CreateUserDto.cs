using App.ApplicationCore.Domain.Entities;
using AutoMapper;
using AutoMapper.Configuration.Annotations;


namespace App.ApplicationCore.Domain.Dtos.UserDtos
{
    [AutoMap(typeof(User))]
    public class CreateUserDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        [Ignore]
       public string Password { get; set; }

    }
}
