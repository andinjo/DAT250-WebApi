using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class CreateReply
    {
        [Required]
        public string Content { get; set; }
    }
}
