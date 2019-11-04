using System;

namespace Models.Requests
{
    public class CreateItemRequest
    {
        public string Definition { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
