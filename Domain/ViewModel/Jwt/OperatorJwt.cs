
using System;

namespace Domain.ViewModel.Jwt
{
    public record OperatorJwt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Registration { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
