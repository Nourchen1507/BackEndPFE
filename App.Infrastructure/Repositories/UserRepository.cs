﻿using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using App.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _users = _applicationDbContext.Set<User>();
        }

        public async Task<User> CreateAdminAsync(User user)
        {
            user.Role = UserRole.Admin;
            var entry = await _users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> UpdatePassword(string email, string PasswordHash)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            user.PasswordHash = PasswordHash;
            await _applicationDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByCIN(string cin)
        {
            return await _applicationDbContext.User.FirstOrDefaultAsync(u => u.CIN == cin);
        }

    }
}