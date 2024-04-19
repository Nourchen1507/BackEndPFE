using App.ApplicationCore.Common;
using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();

        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                if (entity is User userEntity)
                {
                    if (userEntity.Role != UserRole.Admin)
                    {
                        userEntity.Role = UserRole.Client;
                    }
                }
                var entry = await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entry.Entity;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Base Repository exception.");
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                throw;
            }
        }


        public async Task<bool> DeleteByIdAsync(Guid entityId)
        {
            var entityToDelete = await GetByIdAsync(entityId);
            if (entityToDelete == null)
            {
                return false;
            }
            _dbSet.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entities = await _dbSet.AsNoTracking().ToListAsync();
            return entities;
        }


        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            return await _dbSet.FindAsync(entityId);
        }

        public async Task<TEntity> UpdateAsync(Guid entityId, TEntity updatedEntity)
        {
            var existingEntity = await GetByIdAsync(entityId);
            if (existingEntity == null)
            {
                return null;
            }

            var entityProperties = typeof(TEntity).GetProperties();
            foreach (var property in entityProperties)
            {
                var newValue = property.GetValue(updatedEntity);
                if (newValue != null)
                {
                    property.SetValue(updatedEntity, newValue);
                }
            }
            try
            {
                await _dbContext.SaveChangesAsync();
                return existingEntity;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the entity.", ex);
            }
        }
    }
}
