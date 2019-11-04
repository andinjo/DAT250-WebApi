using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dto
{
    public class TodoList
    {
        public TodoList() {}
        public int Id { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
