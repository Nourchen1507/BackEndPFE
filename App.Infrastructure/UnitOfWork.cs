using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbcontext;

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbcontext = context;
           
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}
