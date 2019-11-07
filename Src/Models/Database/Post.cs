using System;
using System.Collections.Generic;

namespace Models.Database
{
    public class Thread
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Forum Forum { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
