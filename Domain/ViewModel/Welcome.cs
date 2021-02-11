using System;

namespace Domain.ViewModel
{
    public record Welcome
    {
        public string Message
        { 
          get
          { 
            return "API Localiza Labs";
          }
        }

        public string Documentation
        { 
          get
          { 
            return "https://localhost:5001/swagger";
          }
        }

        
    }
}
