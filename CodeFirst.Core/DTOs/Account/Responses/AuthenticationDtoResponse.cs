using System;

namespace CodeFirst.Core.DTOs.Account.Responses
{
    public class AuthenticationDtoResponse
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
