using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dto
{
    public class Item
    {
        public Item() {}
        public int Id { get; set; }
        public string Definition { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
