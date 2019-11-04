using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Responses
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
