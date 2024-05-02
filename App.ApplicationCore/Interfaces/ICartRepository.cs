using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface ICartRepository
    {



        Task<ClientCart> GetByIdAsync(int entityId);
        Task<ClientCart> GetCartAsync(int cartId);

            Task<ClientCart> UpdateCartAsync(ClientCart clientCart);

            Task<bool> DeleteCartAsync( int cartId);
        }
    }
