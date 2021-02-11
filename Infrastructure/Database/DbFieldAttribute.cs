using System;

namespace Infrastructure.Database
{
    public class DbFieldAttribute : Attribute
    {
        public string Name { get; set; }
    }
}