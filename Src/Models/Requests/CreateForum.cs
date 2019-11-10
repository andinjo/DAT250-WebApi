using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class CreateForum
    {
        [Required]
        [StringLength(16, MinimumLength = 2)]
        [RegularExpression("[0-9a-zA-Z]+")]
        public string Title { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }
    }
}
