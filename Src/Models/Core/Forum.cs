using System;
using System.Collections.Generic;

namespace Models.Business
{
    public class Forum
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
