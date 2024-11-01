using EcommerceAspNet.Domain.Entitie.Ecommerce;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.User
{
    [Table("users")]
    public class User : IdentityUser<long>
    {
        public bool Active { get; set; } = true;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn {  get; set; } = DateTime.UtcNow;
        [MinLength(8, ErrorMessage = "Password must has 8 or more digits")]
        public string Password { get; set; } = string.Empty;
        public string? ImageIdentifier { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserIdentifier { get; set; } = Guid.NewGuid();
        [InverseProperty("User")]
        public Order? UserOrder { get; set; }
    }
}
