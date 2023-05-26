using CodeFirst.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;

namespace CodeFirst.Infrastructure.Repositories.RespositoryAsync
{
    public class RoleRepositoryAsync : GenericRepository<IdentityRole>
    {
        public RoleRepositoryAsync(CodeFirstContext codeFirstContext) : base(codeFirstContext)
        {

        }
    }
}
