using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("categories")]
    public class CategoryEntitie : BaseEntitie
    {
        [MaxLength(255, ErrorMessage = "Length must be less 255")]
        [Required(ErrorMessage = "Field is required")]
        public string Name { get; set; } = string.Empty;
    }
}
