using EcommerceAspNet.Domain.Entitie.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    [Table("comments")]
    public class CommentEntitie : BaseEntitie
    {
        [MaxLength(255, ErrorMessage = "Comments must have 255 digits or less")]
        public string Text { get; set; } = string.Empty;
        public Int16 Note {  get; set; }
        [ForeignKey("products")]
        public long ProductId { get; set; }
        [ForeignKey("Users")]
        public long? UserId { get; set; }
        [NotMapped]
        public UserEntitie User { get; set; }
    }
}
