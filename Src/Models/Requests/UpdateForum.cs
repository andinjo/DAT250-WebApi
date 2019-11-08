using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class UpdateForum
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
