using CodeFirst.Domain.BaseEntities;
using CodeFirst.Domain.Enums;

namespace CodeFirst.Domain.Entities
{
    public class Security : AuditEntity
    {
        public string UserName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
    }
}