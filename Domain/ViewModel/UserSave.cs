using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public record UserSave
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string CPF { get; set; }
    }
}
