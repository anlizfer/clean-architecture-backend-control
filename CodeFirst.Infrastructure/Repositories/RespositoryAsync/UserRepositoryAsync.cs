using CodeFirst.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;

namespace CodeFirst.Infrastructure.Repositories.RespositoryAsync
{
    public class UserRepositoryAsync : GenericRepository<IdentityUser>
    {
        public UserRepositoryAsync(CodeFirstContext codeFirstContext) : base(codeFirstContext)
        {

        }
    }
}
