using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModel
{
    public record CompleteUser
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        public int IdAddress { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        public string Complement { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}
