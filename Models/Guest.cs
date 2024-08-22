﻿using System.ComponentModel.DataAnnotations;

namespace event_management_asp_project.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Prompt = "Enter Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;
    }
}
