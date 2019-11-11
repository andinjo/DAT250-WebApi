using System;
using System.Collections.Generic;

namespace Models.Business
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual Forum Forum { get; set; }
        public virtual IEnumerable<Reply> Replies { get; set; }
    }
}
