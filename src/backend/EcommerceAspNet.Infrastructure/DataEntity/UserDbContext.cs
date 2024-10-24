﻿using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using EcommerceAspNet.Domain.Repository.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class UserDbContext : IUserReadOnlyRepository, IUnitOfWork, IUserWriteOnlyRepository
    {
        private readonly ProjectDbContext _dbContext;
        public UserDbContext(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserEntitie user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid uid)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.UserIdentifier == uid);

            var orders = _dbContext.Orders.Where(o => o.UserId == user!.Id);

            _dbContext.Orders.RemoveRange(orders);
            _dbContext.Users.Remove(user!);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext
                .Users
                .AnyAsync(x => x.Email == email);
        }

        public async Task<UserEntitie?> LoginByEmail(string email)
        {
            return await _dbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Active && e.Email == email);
        }

        public async Task<bool> PasswordEqual(UserEntitie user, string password)
        {
            return await _dbContext.Users.AnyAsync(x => x.Password == password && user.Id ==  x.Id);
        }

        public void Update(UserEntitie user)
        {
            _dbContext.Update(user);
        }

        public async Task<UserEntitie?> UserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<UserEntitie?> UserByIdentifier(Guid uid)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(d => d.UserIdentifier == uid);
        }

        public async Task<UserEntitie?> UserById(long? id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == username);
        }

        public async Task<List<UserEntitie>> GetAdmins()
        {
            var roleName = await _dbContext.Roles.FirstOrDefaultAsync(d => d.Name == "admin");
            var roles = await _dbContext.UserRoles.Where(d => d.RoleId == roleName.Id).ToListAsync();

            List<UserEntitie> users = new List<UserEntitie>();

            foreach(var role in roles)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == role.UserId);

                if(user is not null)
                    users.Add(user);
            }

            return users;
        }
    }
}
