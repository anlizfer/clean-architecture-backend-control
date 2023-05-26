using CodeFirst.Domain.Settings;

namespace CodeFirst.Core.DTOs.DataProtection.Responses
{
    public class HashDtoResponse
    {
        public string TextoPlano { get; set; }
        public PasswordHash Hash { get; set; }

    }
}
