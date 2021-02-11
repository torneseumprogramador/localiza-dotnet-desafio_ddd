using System;

namespace Domain.ViewModel
{
    public record UserLogin
    {
      public string Document {get;set;}        
      public string Password {get;set;}        
    }
}
