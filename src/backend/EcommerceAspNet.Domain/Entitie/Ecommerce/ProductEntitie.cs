using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Domain.Entitie.Ecommerce
{
    public class ProductEntitie : BaseEntitie
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Stock { get; set; }
        public IList<CategoryEntitie> CategoryId { get; set; } = [];
    }
}
