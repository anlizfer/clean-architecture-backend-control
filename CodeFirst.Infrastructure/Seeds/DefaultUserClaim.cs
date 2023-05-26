using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CodeFirst.Infrastructure.Seeds
{
    public static class DefaultUserClaim
    {
        public static async Task SeedDefaultUserClaimAsync(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                    new IdentityUserClaim<string>
                    {
                        Id = 1,
                        ClaimType = ClaimTypes.Role,
                        UserId = "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                        ClaimValue = "Admin"
                    }
                );
            await Task.FromResult(Task.CompletedTask).ConfigureAwait(false);
        }
    }
}
