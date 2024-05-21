using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class CommentQueryObject
    {
        public string symbol { get; set; }
        public bool IsDescending { get; set; } = true;
        
    }
}