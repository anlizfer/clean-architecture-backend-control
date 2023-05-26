using CodeFirst.Domain.Enums;
using System;

namespace CodeFirst.Core.DTOs.Request.Security
{
    public class SecurityUpdateDtoRequest
    {
        public string UserName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}