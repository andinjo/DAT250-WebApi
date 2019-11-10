using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class CreatePost
    {
        [Required]
        public string Content { get; set; }
    }
}
