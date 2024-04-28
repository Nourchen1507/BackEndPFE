using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
   public interface IJwtManager
    {


        public string GenerateAccessToken(User user);

        Task<string> RefreshAccessToken(string refreshToken);   

    }
}
