
using System;

namespace Domain.ViewModel.Jwt
{
    public record UserJwt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
