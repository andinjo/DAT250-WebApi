using System;

namespace Models.Database
{
    public class Forum
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
