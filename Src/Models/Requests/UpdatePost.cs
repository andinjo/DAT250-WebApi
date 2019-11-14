using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class UpdatePost
    {
        [Required]
        public string Content { get; set; }
    }
}
