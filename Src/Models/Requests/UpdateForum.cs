﻿using System.ComponentModel.DataAnnotations;

namespace Models.Requests
{
    public class UpdateForum
    {
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
    }
}
