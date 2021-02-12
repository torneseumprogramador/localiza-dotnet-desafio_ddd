using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public record OperatorSave
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Registration { get; set; }
    }
}
