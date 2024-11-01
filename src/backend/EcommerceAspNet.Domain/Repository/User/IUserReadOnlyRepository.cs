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
        public Task<List<Entitie.User.User>> GetAdmins();
        public Task<bool> PasswordEqual(Entitie.User.User user, string password);

        public Task<Entitie.User.User?> UserByEmail(string email);

        public Task<bool> UsernameExists(string username);

        public Task<Entitie.User.User?> UserByIdentifier(Guid uid);

        public Task<Entitie.User.User?> LoginByEmail(string email);
        public Task<Entitie.User.User?> UserById(long? id);
    }
}
