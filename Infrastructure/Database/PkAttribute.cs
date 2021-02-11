using System;

namespace Infrastructure.Database
{
    public class PkAttribute : Attribute
    {
        public string Name {get;set;}
    }
}