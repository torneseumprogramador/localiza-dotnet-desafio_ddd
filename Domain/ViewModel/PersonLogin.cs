using System;

namespace Domain.ViewModel
{
    public record PersonLogin
    {
      public string Document { get;set;}        
      public string Password {get;set;}        
    }
}
