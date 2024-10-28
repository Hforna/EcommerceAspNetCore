using EcommerceAspNet.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("products")]
    public class Product : BaseEntitie
    {
        [StringLength(255, ErrorMessage = "Name length must be less 256")]
        public string Name { get; set; } = string.Empty;
        [StringLength(255, ErrorMessage = "Description length must be less 256")]
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public long Stock { get; set; }
        public string? ImageIdentifier { get; set; }
        [ForeignKey("categories")]
        public long CategoryId { get; set; }
        public PriceEnum? groupPrice { get; set; } = 0;
        public IList<CommentEntitie?> Comments { get; set; }
        public Guid ProductIdentifier { get; set; } = Guid.NewGuid();
    }
}
