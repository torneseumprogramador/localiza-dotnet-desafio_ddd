
using System;
using Domain.Entities;
using Infrastructure.Database;

namespace Domain.ViewModel
{
    [Table(Name = "Users")]
    public record OperatorView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public int Type { get; set; }
        public string Role
        {
            get
            {
                return ((PersonRole)Enum.ToObject(typeof(PersonRole), this.Type)).ToString();
            }
        }
    }
}
