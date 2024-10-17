using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.User
{
    public class UserEntitie : IdentityUser<long>
    {
        public string Username { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public DateTime CreatedOn {  get; set; }
        public string Password { get; set; } = string.Empty;
        public string? ImageIdentifier { get; set; }
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }
}
