using CodeFirst.Domain.Enums;

namespace CodeFirst.Core.DTOs.Security
{
    public class SecurityAddDtoRequest
    {
        public string UserName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}