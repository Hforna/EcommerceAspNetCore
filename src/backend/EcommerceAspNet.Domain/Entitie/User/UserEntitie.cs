using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.User
{
    public class UserEntitie : BaseEntitie
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? ImageIdentifier { get; set; }
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
    }
}
