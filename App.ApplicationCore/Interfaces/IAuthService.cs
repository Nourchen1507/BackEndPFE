using App.ApplicationCore.Domain.Dtos;
using App.ApplicationCore.Domain.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseLoginDto> AuthenticateUser(UserCredentialsDto userCredentials);
        Task<string> RefreshToken(string refreshToken);
    }
}
