using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Requests
{
    public class UpdateItemRequest
    {
        public string Definition { get; set; }
        public bool IsDone { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
