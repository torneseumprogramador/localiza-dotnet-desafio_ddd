using System;
using System.Text.Json.Serialization;

namespace Domain.ViewModel
{
    public record Welcome
    {
        [JsonPropertyName("Mensagem")]
        public string Message
        { 
          get
          { 
            return "API Localiza Labs";
          }
        }

        [JsonPropertyName("Documentacao")]
        public string Documentation
        { 
          get
          { 
            return "/swagger";
          }
        }

        
    }
}
