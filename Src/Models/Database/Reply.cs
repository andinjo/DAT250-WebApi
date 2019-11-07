using System;

namespace Models.Database
{
    public class Reply
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Thread Thread { get; set; }
    }
}
