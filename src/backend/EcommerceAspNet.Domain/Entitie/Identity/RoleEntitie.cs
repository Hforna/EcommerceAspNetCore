using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Identity
{
    [Table("roles")]
    public class RoleEntitie : IdentityRole<long>
    {
        public RoleEntitie(string name) => Name = name;
    }
}
