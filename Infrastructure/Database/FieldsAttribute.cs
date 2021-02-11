using System;

namespace Infrastructure.Database
{
    public class FieldsAttribute : Attribute
    {
        public string Name { get; set; }
    }
}