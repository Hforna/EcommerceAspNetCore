using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAspNet.Communication.Response.Comment
{
    public class ResponseComment
    {
        public required string Text { get; set; }
        public int Note {  get; set; }
        public string? Username { get; set; } = "Anonymous";
        public string UserImage { get; set; } = string.Empty;
    }
}
