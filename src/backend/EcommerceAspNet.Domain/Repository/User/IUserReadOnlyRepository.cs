using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Repository.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> EmailExists(string email);

        public Task<bool> PasswordEqual(UserEntitie user, string password);

        public Task<UserEntitie?> UserByEmail(string email);

        public Task<bool> UsernameExists(string username);

        public Task<UserEntitie?> UserByIdentifier(Guid uid);

        public Task<UserEntitie?> LoginByEmail(string email);
        public Task<UserEntitie?> UserById(long? id);
    }
}
