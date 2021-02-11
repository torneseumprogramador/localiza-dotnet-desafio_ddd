using System;

namespace Domain.ViewModel
{
    public record UserLogin
    {
      public string Email {get;set;}        
      public string Password {get;set;}        
    }
}
