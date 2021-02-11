using System;

namespace Infrastructure.Database
{
    public class TableAttribute : Attribute
    {
        public string Name { get; set; }
    }
}