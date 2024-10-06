using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Request.Comment
{
    public class RequestCreateComment
    {
        public string? Text { get; set; }
        public int Note {  get; set; }
        public string? ProductId { get; set; }
    }
}
