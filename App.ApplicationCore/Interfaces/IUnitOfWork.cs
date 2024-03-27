using App.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {

        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        void Commit();
    }
}
