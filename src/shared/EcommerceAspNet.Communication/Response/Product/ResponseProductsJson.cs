using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Product
{
    public class ResponseProductsJson
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public int Count { get; set; }
        public int FirstItemOnPage { get; set; }
        public int LastItemOnPage { get; set; }
        public int TotalItemCount { get; set; }

        public IList<ResponseProductShort> Products { get; set; } = [];
    }
}
