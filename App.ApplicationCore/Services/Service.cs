using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.ApplicationCore.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {

        public IUnitOfWork UnitOfWork { get; private set; }
        private readonly IGenericRepository<T> _repository;

        public Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = unitOfWork.Repository<T>();
        }

        public T GetById(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
    }
}