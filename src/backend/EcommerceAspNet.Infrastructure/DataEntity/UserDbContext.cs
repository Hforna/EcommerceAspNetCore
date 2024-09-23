using EcommerceAspNet.Domain.Entitie.User;
using EcommerceAspNet.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Infrastructure.DataEntity
{
    public class UserDbContext : IUserReadOnlyRepository, IUnitOfWork, IAddUser
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

        public async Task<bool> EmailExists(string email)
        {
            return await _dbContext
                .User
                .AnyAsync(x => x.Email == email);
        }
    }
}
