
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Entities.Enums;
using Domain.Entities.Interfaces;

namespace Domain.Entities
{
    [Table("users")]
    public class Person : IPerson
    {
        [Key]
        [Column]
        public virtual int Id { get; set; }

        [Column]
		[Required]
        [MaxLength(100)]
        [JsonPropertyName("Nome")]
        public virtual string Name { get; set; }

        [Column]
		[Required]
        [MaxLength(15)]
        [JsonPropertyName("Documento")]
        public virtual string Document { get; set; }

        [Column]
		[Required]
        [JsonPropertyName("Tipo")]
        public virtual int Type { get; set; }

        [Column]
		[Required]
        [MaxLength(150)]
        [JsonPropertyName("Senha")]
        public virtual string Password { get; set; }

        [Column]
        [JsonPropertyName("EnderecoId")]
        public virtual int? AddressId { get; set; }

        [JsonPropertyName("TipoDeAcesso")]
        public virtual PersonRole Role { get { return (PersonRole)Enum.ToObject(typeof(PersonRole), this.Type); } }
    }
}
